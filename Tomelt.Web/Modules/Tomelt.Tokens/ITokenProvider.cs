using Tomelt.Events;

namespace Tomelt.Tokens {
    public interface ITokenProvider : IEventHandler {
        void Describe(DescribeContext context);
        void Evaluate(EvaluateContext context);
    }
}