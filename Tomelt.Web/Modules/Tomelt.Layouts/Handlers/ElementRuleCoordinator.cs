using System;
using System.Collections.Generic;
using Tomelt.Conditions.Services;
using Tomelt.Layouts.Framework.Display;
using Tomelt.Layouts.Services;

namespace Tomelt.Layouts.Handlers {
    public class ElementRuleCoordinator : ElementEventHandlerBase {
        private readonly IConditionManager _conditionManager;
        private readonly Dictionary<string, bool> _evaluations = new Dictionary<string, bool>();

        public ElementRuleCoordinator(IConditionManager conditionManager) {
            _conditionManager = conditionManager;
        }

        public override void CreatingDisplay(ElementCreatingDisplayShapeContext context) {
            if (context.DisplayType == "Design")
                return;

            if (String.IsNullOrWhiteSpace(context.Element.Rule))
                return;

            context.Cancel = !EvaluateRule(context.Element.Rule);
        }

        private bool EvaluateRule(string rule) {
            if (_evaluations.ContainsKey(rule))
                return _evaluations[rule];

            var result = _conditionManager.Matches(rule);
            _evaluations[rule] = result;
            return result;
        }
    }
}