using Tomelt.Events;

namespace Tomelt.Recipes.Events {
    public interface IRecipeSchedulerEventHandler : IEventHandler  {
        void ExecuteWork(string executionId);
    }
}
