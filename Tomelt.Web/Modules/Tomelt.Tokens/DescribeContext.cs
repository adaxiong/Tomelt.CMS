using System.Collections.Generic;
using Tomelt.Localization;

namespace Tomelt.Tokens {
    public abstract class DescribeContext {
        public abstract IEnumerable<TokenTypeDescriptor> Describe(params string[] targets);
        public abstract DescribeFor For(string target);
        public abstract DescribeFor For(string target, LocalizedString name, LocalizedString description);
    }
}