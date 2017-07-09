using System;
using Tomelt.Logging;
using Tomelt.Recipes.Models;
using Tomelt.Recipes.Services;
using Tomelt.Themes.Services;

namespace Tomelt.Themes.Recipes.Executors {
    public class CurrentThemeStep : RecipeExecutionStep {
        private readonly ISiteThemeService _siteThemeService;

        public override string Name {
            get { return "CurrentTheme"; }
        }

        public CurrentThemeStep(
            ISiteThemeService siteThemeService,
            RecipeExecutionLogger logger) : base(logger) {
            _siteThemeService = siteThemeService;
        }

        public override void Execute(RecipeExecutionContext context) {
            var themeId = context.RecipeStep.Step.Attribute("id").Value;
            Logger.Information("Setting site theme to '{0}'.", themeId);

            try {
                _siteThemeService.SetSiteTheme(themeId);
            }
            catch (Exception ex) {
                Logger.Error(ex, "Error while setting site theme to '{0}'.", themeId);
                throw;
            }
        }
    }
}