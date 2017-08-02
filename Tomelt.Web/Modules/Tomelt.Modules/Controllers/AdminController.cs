using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Tomelt.Data.Migration;
using Tomelt.DisplayManagement;
using Tomelt.Environment.Configuration;
using Tomelt.Environment.Descriptor.Models;
using Tomelt.Environment.Extensions;
using Tomelt.Environment.Extensions.Models;
using Tomelt.Environment.Features;
using Tomelt.Localization;
using Tomelt.Logging;
using Tomelt.Modules.Events;
using Tomelt.Modules.Models;
using Tomelt.Modules.Services;
using Tomelt.Modules.ViewModels;
using Tomelt.Mvc;
using Tomelt.Mvc.AntiForgery;
using Tomelt.Mvc.Extensions;
using Tomelt.Recipes.Models;
using Tomelt.Recipes.Services;
using Tomelt.Security;
using Tomelt.UI.Navigation;
using Tomelt.UI.Notify;

namespace Tomelt.Modules.Controllers
{
    public class AdminController : Controller
    {
        private readonly IExtensionDisplayEventHandler _extensionDisplayEventHandler;
        private readonly IModuleService _moduleService;
        private readonly IDataMigrationManager _dataMigrationManager;
        private readonly IExtensionManager _extensionManager;
        private readonly IFeatureManager _featureManager;
        private readonly IRecipeHarvester _recipeHarvester;
        private readonly IRecipeManager _recipeManager;
        private readonly ShellDescriptor _shellDescriptor;
        private readonly ShellSettings _shellSettings;

        public AdminController(
            IEnumerable<IExtensionDisplayEventHandler> extensionDisplayEventHandlers,
            ITomeltServices services,
            IModuleService moduleService,
            IDataMigrationManager dataMigrationManager,
            IExtensionManager extensionManager,
            IFeatureManager featureManager,
            IRecipeHarvester recipeHarvester,
            IRecipeManager recipeManager,
            ShellDescriptor shellDescriptor,
            ShellSettings shellSettings,
            IShapeFactory shapeFactory)
        {
            Services = services;
            _extensionDisplayEventHandler = extensionDisplayEventHandlers.FirstOrDefault();
            _moduleService = moduleService;
            _dataMigrationManager = dataMigrationManager;
            _extensionManager = extensionManager;
            _featureManager = featureManager;
            _recipeHarvester = recipeHarvester;
            _recipeManager = recipeManager;
            _shellDescriptor = shellDescriptor;
            _shellSettings = shellSettings;
            Shape = shapeFactory;

            T = NullLocalizer.Instance;
            Logger = NullLogger.Instance;
        }

        public Localizer T { get; set; }
        public ITomeltServices Services { get; set; }
        public ILogger Logger { get; set; }
        public dynamic Shape { get; set; }

        public ActionResult Index(ModulesIndexOptions options, PagerParameters pagerParameters)
        {
            if (!Services.Authorizer.Authorize(StandardPermissions.SiteOwner, T("Not allowed to manage modules")))
                return new HttpUnauthorizedResult();

            Pager pager = new Pager(Services.WorkContext.CurrentSite, pagerParameters);

            IEnumerable<ModuleEntry> modules = _extensionManager.AvailableExtensions()
                .Where(extensionDescriptor => DefaultExtensionTypes.IsModule(extensionDescriptor.ExtensionType) &&

                                              (string.IsNullOrEmpty(options.SearchText) || extensionDescriptor.Name.ToLowerInvariant().Contains(options.SearchText.ToLowerInvariant())))
                .OrderBy(extensionDescriptor => extensionDescriptor.Name)
                .Select(extensionDescriptor => new ModuleEntry { Descriptor = extensionDescriptor });

            int totalItemCount = modules.Count();

            if (pager.PageSize != 0)
            {
                modules = modules.Skip((pager.Page - 1) * pager.PageSize).Take(pager.PageSize);
            }

            // This way we can more or less reliably handle this implicit dependency.
            var installModules = _featureManager.GetEnabledFeatures().FirstOrDefault(f => f.Id == "PackagingServices") != null;

            modules = modules.ToList();
            foreach (ModuleEntry moduleEntry in modules)
            {
                moduleEntry.IsRecentlyInstalled = _moduleService.IsRecentlyInstalled(moduleEntry.Descriptor);
                moduleEntry.CanUninstall = installModules;

                if (_extensionDisplayEventHandler != null)
                {
                    foreach (string notification in _extensionDisplayEventHandler.Displaying(moduleEntry.Descriptor, ControllerContext.RequestContext))
                    {
                        moduleEntry.Notifications.Add(notification);
                    }
                }
            }


            return View(new ModulesIndexViewModel
            {
                Modules = modules,
                InstallModules = installModules,
                Options = options,
                Pager = Shape.Pager(pager).TotalItemCount(totalItemCount)
            });
        }

        public ActionResult Recipes()
        {
            if (!Services.Authorizer.Authorize(StandardPermissions.SiteOwner, T("Not allowed to manage modules")))
                return new HttpUnauthorizedResult();

            IEnumerable<ModuleEntry> modules = _extensionManager.AvailableExtensions()
                .Where(extensionDescriptor => ExtensionIsAllowed(extensionDescriptor))
                .OrderBy(extensionDescriptor => extensionDescriptor.Name)
                .Select(extensionDescriptor => new ModuleEntry { Descriptor = extensionDescriptor });

            var viewModel = new RecipesViewModel();

            if (_recipeHarvester != null)
            {
                viewModel.Modules = modules.Select(x => new ModuleRecipesViewModel
                {
                    Module = x,
                    Recipes = _recipeHarvester.HarvestRecipes(x.Descriptor.Id).Where(recipe => !recipe.IsSetupRecipe).ToList()
                })
                .Where(x => x.Recipes.Any())
                .ToList();
            }

            return View(viewModel);

        }

        [HttpPost, ActionName("Recipes")]
        public ActionResult RecipesPOST(string moduleId, string name)
        {
            if (!Services.Authorizer.Authorize(StandardPermissions.SiteOwner, T("Not allowed to manage modules")))
                return new HttpUnauthorizedResult();

            ModuleEntry module = _extensionManager.AvailableExtensions()
                .Where(extensionDescriptor => extensionDescriptor.Id == moduleId && ExtensionIsAllowed(extensionDescriptor))
                .Select(extensionDescriptor => new ModuleEntry { Descriptor = extensionDescriptor }).FirstOrDefault();

            if (module == null)
            {
                return HttpNotFound();
            }

            Recipe recipe = _recipeHarvester.HarvestRecipes(module.Descriptor.Id).FirstOrDefault(x => !x.IsSetupRecipe && x.Name == name);

            if (recipe == null)
            {
                return HttpNotFound();
            }

            try
            {
                _recipeManager.Execute(recipe);
            }
            catch (Exception e)
            {
                Logger.Error(e, "Error while executing recipe {0} in {1}", moduleId, name);
                Services.Notifier.Error(T("Recipes {0} contains  unsupported module installation steps.", recipe.Name));
            }

            Services.Notifier.Information(T("The recipe {0} was executed successfully.", recipe.Name));

            return RedirectToAction("Recipes");

        }
        public ActionResult Features()
        {
            if (!Services.Authorizer.Authorize(Permissions.ManageFeatures, T("Not allowed to manage features")))
                return new HttpUnauthorizedResult();

            var featuresThatNeedUpdate = _dataMigrationManager.GetFeaturesThatNeedUpdate();

            IEnumerable<ModuleFeature> features = _featureManager.GetAvailableFeatures()
                .Where(f => !DefaultExtensionTypes.IsTheme(f.Extension.ExtensionType))
                .Select(f => new ModuleFeature
                {
                    Descriptor = f,//描述信息
                    IsEnabled = _shellDescriptor.Features.Any(sf => sf.Name == f.Id),//是否启用
                    IsRecentlyInstalled = _moduleService.IsRecentlyInstalled(f.Extension),//是否最近安装
                    NeedsUpdate = featuresThatNeedUpdate.Contains(f.Id),//是否升级
                    DependentFeatures = _moduleService.GetDependentFeatures(f.Id).Where(x => x.Id != f.Id).ToList()
                })
                .ToList();

            return View(new FeaturesViewModel
            {
                Features = features,
                IsAllowed = ExtensionIsAllowed
            });
        }
        public ActionResult List()
        {
            if (!Services.Authorizer.Authorize(Permissions.ManageFeatures, T("无查看权限")))
                return new HttpUnauthorizedResult();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryTokenTomelt(false)]
        public ActionResult GetList(string name = "")
        {
            if (!Services.Authorizer.Authorize(Permissions.ManageFeatures, T("无查看权限")))
                return new HttpUnauthorizedResult();
            var featuresThatNeedUpdate = _dataMigrationManager.GetFeaturesThatNeedUpdate();
            IEnumerable<ModuleFeature> features = _featureManager.GetAvailableFeatures()
                .Where(f => !DefaultExtensionTypes.IsTheme(f.Extension.ExtensionType))
                .Select(f => new ModuleFeature
                {
                    Descriptor = f,
                    IsEnabled = _shellDescriptor.Features.Any(sf => sf.Name == f.Id),
                    IsRecentlyInstalled = _moduleService.IsRecentlyInstalled(f.Extension),
                    NeedsUpdate = featuresThatNeedUpdate.Contains(f.Id),
                    DependentFeatures = _moduleService.GetDependentFeatures(f.Id).Where(x => x.Id != f.Id).ToList()
                })
                .ToList();
            if (!string.IsNullOrWhiteSpace(name))
            {
                features = features.Where(d => d.Descriptor.Name.ToLower().Contains(name.ToLower()));
            }
            var rows = features.Select(d => new
            {
                Category= string.IsNullOrWhiteSpace(d.Descriptor.Category)?T("其他").ToString():d.Descriptor.Category,
                d.Descriptor.Id,
                d.Descriptor.Name,
                d.Descriptor.Description,
                d.Descriptor.Priority,
                Dependencies = string.Join(",", d.Descriptor.Dependencies),
                d.IsEnabled,
                ShowEnable= ExtensionIsAllowed(d.Descriptor.Extension) && d.Descriptor.Dependencies.All(x => features.Any(f => f.Descriptor.Id.Equals(x, StringComparison.OrdinalIgnoreCase))) && d.Descriptor.Id != "Tomelt.Setup",
                d.IsRecentlyInstalled,
                d.NeedsUpdate,
                IsAllowed= ExtensionIsAllowed(d.Descriptor.Extension)
                //d.DependentFeatures,


            }).OrderBy(d => d.Category);
            return Json(rows);
        }
        [HttpPost, ActionName("Features")]
        [FormValueRequired("submit.BulkExecute")]
        public ActionResult FeaturesPOST(FeaturesBulkAction bulkAction, IList<string> featureIds, bool? force)
        {

            if (!Services.Authorizer.Authorize(Permissions.ManageFeatures, T("Not allowed to manage features")))
                return new HttpUnauthorizedResult();

            if (featureIds == null || !featureIds.Any())
            {
                ModelState.AddModelError("featureIds", T("Please select one or more features."));
            }

            if (ModelState.IsValid)
            {
                var availableFeatures = _moduleService.GetAvailableFeatures().Where(feature => ExtensionIsAllowed(feature.Descriptor.Extension)).ToList();
                var selectedFeatures = availableFeatures.Where(x => featureIds.Contains(x.Descriptor.Id)).ToList();
                var enabledFeatures = availableFeatures.Where(x => x.IsEnabled && featureIds.Contains(x.Descriptor.Id)).Select(x => x.Descriptor.Id).ToList();
                var disabledFeatures = availableFeatures.Where(x => !x.IsEnabled && featureIds.Contains(x.Descriptor.Id)).Select(x => x.Descriptor.Id).ToList();

                switch (bulkAction)
                {
                    case FeaturesBulkAction.None:
                        break;
                    case FeaturesBulkAction.Enable:
                        _moduleService.EnableFeatures(disabledFeatures, force == true);
                        break;
                    case FeaturesBulkAction.Disable:
                        _moduleService.DisableFeatures(enabledFeatures, force == true);
                        break;
                    case FeaturesBulkAction.Toggle:
                        _moduleService.EnableFeatures(disabledFeatures, force == true);
                        _moduleService.DisableFeatures(enabledFeatures, force == true);
                        break;
                    case FeaturesBulkAction.Update:
                        var featuresThatNeedUpdate = _dataMigrationManager.GetFeaturesThatNeedUpdate();
                        var selectedFeaturesThatNeedUpdate = selectedFeatures.Where(x => featuresThatNeedUpdate.Contains(x.Descriptor.Id));

                        foreach (var feature in selectedFeaturesThatNeedUpdate)
                        {
                            var id = feature.Descriptor.Id;
                            try
                            {
                                _dataMigrationManager.Update(id);
                                Services.Notifier.Information(T("The feature {0} was updated successfully", id));
                            }
                            catch (Exception exception)
                            {
                                Services.Notifier.Error(T("An error occurred while updating the feature {0}: {1}", id, exception.Message));
                            }
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return RedirectToAction("Features");
        }
        [HttpPost]
        [ValidateAntiForgeryTokenTomelt(false)]
        public ActionResult FeaturesAJAX(string action, string featureId, bool? force)
        {

            if (!Services.Authorizer.Authorize(Permissions.ManageFeatures, T("Not allowed to manage features")))
                return Json(new { State = 0, Msg = "无权限执行该操作" });

            if (string.IsNullOrWhiteSpace(featureId))
            {
                return Json(new { State = 0, Msg = "请选择一个模块" });
            }

            var availableFeatures = _moduleService.GetAvailableFeatures().Where(feature => ExtensionIsAllowed(feature.Descriptor.Extension)).ToList();
            var selectedFeatures = availableFeatures.Where(x => x.Descriptor.Id==featureId).ToList();
            var enabledFeatures = availableFeatures.Where(x => x.IsEnabled && featureId==x.Descriptor.Id).Select(x => x.Descriptor.Id).ToList();
            var disabledFeatures = availableFeatures.Where(x => !x.IsEnabled && featureId==x.Descriptor.Id).Select(x => x.Descriptor.Id).ToList();
            if (action== "Enable")
            {
                _moduleService.EnableFeatures(disabledFeatures, force == true);
                return Json(new { State = 1, Msg = T("模块 {0} 启用成功！", featureId).Text });
            }
            if (action == "Disable")
            {
                _moduleService.DisableFeatures(enabledFeatures, force == true);
                return Json(new { State = 1, Msg = T("模块 {0} 禁用成功！", featureId).Text });
            }
            if (action == "Update")
            {
                var featuresThatNeedUpdate = _dataMigrationManager.GetFeaturesThatNeedUpdate();
                var selectedFeaturesThatNeedUpdate = selectedFeatures.Where(x => featuresThatNeedUpdate.Contains(x.Descriptor.Id));

                foreach (var feature in selectedFeaturesThatNeedUpdate)
                {
                    var id = feature.Descriptor.Id;
                    try
                    {
                        _dataMigrationManager.Update(id);
                        return Json(new { State = 1, Msg = T("模块 {0} 更新成功！", id).Text });

                    }
                    catch (Exception exception)
                    {
                        return Json(new { State = 0, Msg = T("模块 {0} 更新异常，异常信息：{1}！", id, exception.Message).Text });
                    }
                }
            }
            return Json(new { State = 0, Msg = "请选择执行动作" });


        }
        /// <summary>
        /// Checks whether the module is allowed for the current tenant
        /// </summary>
        private bool ExtensionIsAllowed(ExtensionDescriptor extensionDescriptor)
        {
            return _shellSettings.Modules.Length == 0 || _shellSettings.Modules.Contains(extensionDescriptor.Id);
        }
    }
}