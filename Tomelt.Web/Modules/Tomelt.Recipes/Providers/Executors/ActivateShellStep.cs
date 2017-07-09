using Tomelt.Environment.Configuration;
using Tomelt.Recipes.Models;
using Tomelt.Recipes.Services;

namespace Tomelt.Recipes.Providers.Executors {
    public class ActivateShellStep : RecipeExecutionStep {
        private readonly ShellSettings _shellSettings;
        private readonly IShellSettingsManager _shellSettingsManager;

        public ActivateShellStep(ShellSettings shellSettings, IShellSettingsManager shellSettingsManager, RecipeExecutionLogger logger) 
            : base(logger) {
            _shellSettings = shellSettings;
            _shellSettingsManager = shellSettingsManager;
        }

        public override string Name { get { return "ActivateShell"; } }

        public override void Execute(RecipeExecutionContext context) {
            _shellSettings.State = TenantState.Running;
            _shellSettingsManager.SaveSettings(_shellSettings);
        }
    }
}
