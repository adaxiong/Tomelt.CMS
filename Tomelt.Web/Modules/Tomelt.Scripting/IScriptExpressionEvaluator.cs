using System.Collections.Generic;

namespace Tomelt.Scripting {
    public interface IScriptExpressionEvaluator : ISingletonDependency {
        object Evaluate(string expression, IEnumerable<IGlobalMethodProvider> providers);
    }
}