using System;

namespace Tomelt.Widgets.Services {
    [Obsolete("Use Tomelt.Conditions.Services.ConditionEvaluationContext instead.")]
    public class RuleContext {
        public string FunctionName { get; set; }
        public object[] Arguments { get; set; }
        public object Result { get; set; }
    }
}