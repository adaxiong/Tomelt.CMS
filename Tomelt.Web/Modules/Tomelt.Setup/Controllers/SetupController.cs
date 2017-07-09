using System;
using System.Linq;
using System.Web.Mvc;
using Tomelt.Environment;
using Tomelt.Environment.Configuration;
using Tomelt.Logging;
using Tomelt.Setup.Services;
using Tomelt.Setup.ViewModels;
using Tomelt.Localization;
using Tomelt.Recipes.Services;
using Tomelt.Themes;
using Tomelt.UI.Notify;

namespace Tomelt.Setup.Controllers {
    [ValidateInput(false), Themed]
    public class SetupController : Controller {
        private readonly IViewsBackgroundCompilation _viewsBackgroundCompilation;
        private readonly ShellSettings _shellSettings;
        private readonly INotifier _notifier;
        private readonly ISetupService _setupService;
        private const string DefaultRecipe = "Default";

        public SetupController(
            INotifier notifier,
            ISetupService setupService,
            IViewsBackgroundCompilation viewsBackgroundCompilation,
            ShellSettings shellSettings) {

            _viewsBackgroundCompilation = viewsBackgroundCompilation;
            _shellSettings = shellSettings;
            _notifier = notifier;
            _setupService = setupService;

            T = NullLocalizer.Instance;
            Logger = NullLogger.Instance;
            RecipeExecutionTimeout = 600;
        }

        public Localizer T { get; set; }
        public ILogger Logger { get; set; }
        public int RecipeExecutionTimeout { get; set; }

        private ActionResult IndexViewResult(SetupViewModel model) {
            return View(model);
        }

        public ActionResult Index() {
            var initialSettings = _setupService.Prime();
            var recipes = _setupService.Recipes().ToList();
            string recipeDescription = null;

            if (recipes.Any()) {
                recipeDescription = recipes[0].Description;
            }

            // On the first time installation of Tomelt, the user gets to the setup screen, which
            // will take a while to finish (user inputting data and the setup process itself).
            // We use this opportunity to start a background task to "pre-compile" all the known
            // views in the app folder, so that the application is more reponsive when the user
            // hits the homepage and admin screens for the first time).
            if (StringComparer.OrdinalIgnoreCase.Equals(initialSettings.Name, ShellSettings.DefaultName)) {
                _viewsBackgroundCompilation.Start();
            }

            return IndexViewResult(new SetupViewModel {
                AdminUsername = "admin",
                DatabaseIsPreconfigured = !String.IsNullOrEmpty(initialSettings.DataProvider),
                Recipes = recipes,
                RecipeDescription = recipeDescription
            });
        }

        [HttpPost, ActionName("Index")]
        public ActionResult IndexPOST(SetupViewModel model) {
            // Sets the setup request timeout to a configurable amount of seconds to give enough time to execute custom recipes.
            HttpContext.Server.ScriptTimeout = RecipeExecutionTimeout;

            var recipes = _setupService.Recipes().ToList();

            // If no builtin provider, a connection string is mandatory.
            if (model.DatabaseProvider != SetupDatabaseType.Builtin && string.IsNullOrEmpty(model.DatabaseConnectionString))
                ModelState.AddModelError("DatabaseConnectionString", T("数据库连接字符串未填.").Text);

            if (!String.IsNullOrWhiteSpace(model.ConfirmPassword) && model.AdminPassword != model.ConfirmPassword) {
                ModelState.AddModelError("ConfirmPassword", T("密码不一致.").Text);
            }

            if (model.DatabaseProvider != SetupDatabaseType.Builtin && !string.IsNullOrWhiteSpace(model.DatabaseTablePrefix)) {
                model.DatabaseTablePrefix = model.DatabaseTablePrefix.Trim();
                if (!Char.IsLetter(model.DatabaseTablePrefix[0])) {
                    ModelState.AddModelError("DatabaseTablePrefix", T("表前缀必须以字母开头.").Text);
                }

                if (model.DatabaseTablePrefix.Any(x => !Char.IsLetterOrDigit(x))) {
                    ModelState.AddModelError("DatabaseTablePrefix", T("表前缀必须包含字母或数字.").Text);
                }
            }
            if (model.Recipe == null) {
                if (!(recipes.Select(r => r.Name).Contains(DefaultRecipe))) {
                    ModelState.AddModelError("Recipe", T("安装模式不存在.").Text);
                }
                else {
                    model.Recipe = DefaultRecipe;
                }
            }
            if (!ModelState.IsValid) {
                model.Recipes = recipes;
                foreach (var recipe in recipes.Where(recipe => recipe.Name == model.Recipe)) {
                    model.RecipeDescription = recipe.Description;
                }
                model.DatabaseIsPreconfigured = !String.IsNullOrEmpty(_setupService.Prime().DataProvider);

                return IndexViewResult(model);
            }

            try {
                string providerName = null;

                switch (model.DatabaseProvider) {
                    case SetupDatabaseType.Builtin:
                        providerName = "SqlCe";
                        break;

                    case SetupDatabaseType.SqlServer:
                        providerName = "SqlServer";
                        break;

                    case SetupDatabaseType.MySql:
                        providerName = "MySql";
                        break;

                    case SetupDatabaseType.PostgreSql:
                        providerName = "PostgreSql";
                        break;

                    default:
                        throw new ApplicationException("Unknown database type: " + model.DatabaseProvider);
                }

                var recipe = recipes.GetRecipeByName(model.Recipe);
                var setupContext = new SetupContext {
                    SiteName = model.SiteName,
                    AdminUsername = model.AdminUsername,
                    AdminPassword = model.AdminPassword,
                    DatabaseProvider = providerName,
                    DatabaseConnectionString = model.DatabaseConnectionString,
                    DatabaseTablePrefix = model.DatabaseTablePrefix,
                    EnabledFeatures = null, // Default list
                    Recipe = recipe
                };

                var executionId = _setupService.Setup(setupContext);

                // First time installation if finally done. Tell the background views compilation
                // process to stop, so that it doesn't interfere with the user (asp.net compilation
                // uses a "single lock" mechanism for compiling views).
                _viewsBackgroundCompilation.Stop();

                // Redirect to the welcome page.
                return Redirect("~/" + _shellSettings.RequestUrlPrefix);
            }
            catch (Exception ex) {
                Logger.Error(ex, "Setup failed");
                _notifier.Error(T("安装失败: {0}", ex.Message));

                model.Recipes = recipes;
                foreach (var recipe in recipes.Where(recipe => recipe.Name == model.Recipe)) {
                    model.RecipeDescription = recipe.Description;
                }
                model.DatabaseIsPreconfigured = !String.IsNullOrEmpty(_setupService.Prime().DataProvider);

                return IndexViewResult(model);
            }
        }
    }
}