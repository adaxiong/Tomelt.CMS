using Tomelt.Events;

namespace Tomelt.Conditions.Services {
    public interface IConditionProvider : IEventHandler {
        void Evaluate(ConditionEvaluationContext evaluationContext);
    }
}