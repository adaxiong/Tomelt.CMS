using System;
using Tomelt.Conditions.Services;
using Tomelt.Widgets.Handlers;

namespace Tomelt.Widgets.Conditions {
    public class ContentDisplayedRuleProvider : IConditionProvider {
        private readonly IDisplayedContentItemHandler _displayedContentItemHandler;

        public ContentDisplayedRuleProvider(IDisplayedContentItemHandler displayedContentItemHandler) {
            _displayedContentItemHandler = displayedContentItemHandler;
        }

        public void Evaluate(ConditionEvaluationContext evaluationContext) {
            if (!String.Equals(evaluationContext.FunctionName, "contenttype", StringComparison.OrdinalIgnoreCase)) {
                return;
            }

            var contentType = Convert.ToString(evaluationContext.Arguments[0]);

            evaluationContext.Result = _displayedContentItemHandler.IsDisplayed(contentType);
        }
    }
}