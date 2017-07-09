using System;
using System.Collections.Generic;
using System.Linq;
using Tomelt.Localization;
using Tomelt.Scripting;

namespace Tomelt.Widgets.Services {
    [Obsolete("Use Tomelt.Conditions.Services.ConditionManager instead.")]
    public class RuleManager : IRuleManager {
        private readonly IRuleProvider _ruleProviders;
        private readonly IEnumerable<IScriptExpressionEvaluator> _evaluators;

        public RuleManager(IRuleProvider ruleProviders, IEnumerable<IScriptExpressionEvaluator> evaluators) {
            _ruleProviders = ruleProviders;
            _evaluators = evaluators;
            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        public bool Matches(string expression) {
            var evaluator = _evaluators.FirstOrDefault();
            if (evaluator == null) {
                throw new TomeltException(T("There are currently no scripting engines enabled"));
            }

            var result = evaluator.Evaluate(expression, new List<IGlobalMethodProvider> { new GlobalMethodProvider(this) });
            if (!(result is bool)) {
                throw new TomeltException(T("Expression is not a boolean value"));
            }
            return (bool)result;
        }

        private class GlobalMethodProvider : IGlobalMethodProvider {
            private readonly RuleManager _ruleManager;

            public GlobalMethodProvider(RuleManager ruleManager) {
                _ruleManager = ruleManager;
            }

            public void Process(GlobalMethodContext context) {
                var ruleContext = new RuleContext {
                    FunctionName = context.FunctionName,
                    Arguments = context.Arguments.ToArray(),
                    Result = context.Result
                };

                _ruleManager._ruleProviders.Process(ruleContext);

                context.Result = ruleContext.Result;
            }
        }
    }
}