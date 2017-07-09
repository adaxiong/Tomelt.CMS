using System;
using Tomelt.Events;

namespace Tomelt.Widgets.Services {
    [Obsolete("Use Tomelt.Conditions.Services.IConditionProvider instead.")]
    public interface IRuleProvider : IEventHandler {
        void Process(RuleContext ruleContext);
    }
}