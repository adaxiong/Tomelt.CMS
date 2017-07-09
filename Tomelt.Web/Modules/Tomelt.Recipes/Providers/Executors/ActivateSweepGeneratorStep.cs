using Tomelt.Recipes.Models;
using Tomelt.Recipes.Services;
using Tomelt.Tasks;

namespace Tomelt.Recipes.Providers.Executors {
    public class ActivateSweepGeneratorStep : RecipeExecutionStep {
        private readonly ISweepGenerator _sweepGenerator;

        public ActivateSweepGeneratorStep(ISweepGenerator sweepGenerator, RecipeExecutionLogger logger) 
            : base(logger) {
            _sweepGenerator = sweepGenerator;
        }

        public override string Name { get { return "ActivateSweepGenerator"; } }

        public override void Execute(RecipeExecutionContext context) {
            _sweepGenerator.Activate();
        }
    }
}
