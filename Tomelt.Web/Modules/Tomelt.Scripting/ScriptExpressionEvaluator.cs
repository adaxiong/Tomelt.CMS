using System;
using System.Collections.Generic;
using System.Linq;
using Tomelt.Caching;
using Tomelt.Environment.Extensions;
using Tomelt.Localization;
using Tomelt.Scripting.Ast;
using Tomelt.Scripting.Compiler;

namespace Tomelt.Scripting {
    [TomeltFeature("Tomelt.Scripting.Lightweight")]
    public class ScriptExpressionEvaluator : IScriptExpressionEvaluator {
        private readonly ICacheManager _cacheManager;

        public ScriptExpressionEvaluator(ICacheManager cacheManager) {
            _cacheManager = cacheManager;
            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        public object Evaluate(string expression, IEnumerable<IGlobalMethodProvider> providers) {
            var expr = _cacheManager.Get(expression, true, ctx => {
                    var ast = ParseExpression(expression);
                    return new { Tree = ast, Errors = ast.GetErrors().ToList() };
                });

            if (expr.Errors.Any()) {
                //TODO: Collect all errors
                throw new TomeltException(T("Syntax error: {0}", expr.Errors.First().Message));
            }

            var result = EvaluateExpression(expr.Tree, providers);
            if (result.IsError) {
                throw new ApplicationException(result.ErrorValue.Message);
            }

            return result.Value;
        }

        private AbstractSyntaxTree ParseExpression(string expression) {
            return new Parser(expression).Parse();
        }

        private EvaluationResult EvaluateExpression(AbstractSyntaxTree tree, IEnumerable<IGlobalMethodProvider> providers) {
            var context = new EvaluationContext {
                Tree = tree,
                MethodInvocationCallback = (m, args) => Evaluate(providers, m, args)
            };
            return new Interpreter().Evalutate(context);
        }

        private object Evaluate(IEnumerable<IGlobalMethodProvider> globalMethodProviders, string name, IEnumerable<object> args) {
            var globalMethodContext = new GlobalMethodContext {
                FunctionName = name, 
                Arguments = args.ToArray(),
                Result = null
            };

            foreach (var globalMethodProvider in globalMethodProviders) {
                globalMethodProvider.Process(globalMethodContext);
            }

            return globalMethodContext.Result;
        }
    }
}