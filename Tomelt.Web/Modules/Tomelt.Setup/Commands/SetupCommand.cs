using System;
using System.Collections.Generic;
using System.Linq;
using Tomelt.Commands;
using Tomelt.Recipes.Services;
using Tomelt.Setup.Services;

namespace Tomelt.Setup.Commands {
    public class SetupCommand : DefaultTomeltCommandHandler {
        private readonly ISetupService _setupService;
        private readonly IRecipeHarvester _recipeHarvester;

        public SetupCommand(ISetupService setupService, IRecipeHarvester recipeHarvester) {
            _setupService = setupService;
            _recipeHarvester = recipeHarvester;
        }

        [TomeltSwitch]
        public string SiteName { get; set; }

        [TomeltSwitch]
        public string AdminUsername { get; set; }

        [TomeltSwitch]
        public string AdminPassword { get; set; }

        [TomeltSwitch]
        public string DatabaseProvider { get; set; }

        [TomeltSwitch]
        public string DatabaseConnectionString { get; set; }

        [TomeltSwitch]
        public string DatabaseTablePrefix { get; set; }

        [TomeltSwitch]
        public string EnabledFeatures { get; set; }

        [TomeltSwitch]
        public string Recipe { get; set; }

        [CommandHelp("setup /SiteName:<siteName> /AdminUsername:<username> /AdminPassword:<password> /DatabaseProvider:<SqlCe|SQLServer|MySql|PostgreSql> " + 
            "/DatabaseConnectionString:<connection_string> /DatabaseTablePrefix:<table_prefix> /EnabledFeatures:<feature1,feature2,...> " +
            "/Recipe:<recipe>" + 
            "\r\n\tRuns first time setup for the site or for a given tenant.")]
        [CommandName("setup")]
        [TomeltSwitches("SiteName,AdminUsername,AdminPassword,DatabaseProvider,DatabaseConnectionString,DatabaseTablePrefix,EnabledFeatures,Recipe")]
        public void Setup() {
            IEnumerable<string> enabledFeatures = null;
            if (!String.IsNullOrEmpty(EnabledFeatures)) {
                enabledFeatures = EnabledFeatures
                    .Split(',')
                    .Select(s => s.Trim())
                    .Where(s => !String.IsNullOrEmpty(s));
            }
            Recipe = String.IsNullOrEmpty(Recipe) ? "Default" : Recipe;
            var recipe = _setupService.Recipes().GetRecipeByName(Recipe);

            var setupContext = new SetupContext {
                SiteName = SiteName,
                AdminUsername = AdminUsername,
                AdminPassword = AdminPassword,
                DatabaseProvider = DatabaseProvider,
                DatabaseConnectionString = DatabaseConnectionString,
                DatabaseTablePrefix = DatabaseTablePrefix,
                EnabledFeatures = enabledFeatures,
                Recipe = recipe,
            };

            var executionId = _setupService.Setup(setupContext);

            Context.Output.WriteLine(T("Setup of site '{0}' was started with recipe execution ID {1}. Use the 'recipes result' command to check the result of the execution.", setupContext.SiteName, executionId));
        }
    }
}
