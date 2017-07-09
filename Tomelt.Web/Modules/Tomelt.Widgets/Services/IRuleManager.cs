using System;

namespace Tomelt.Widgets.Services {
    [Obsolete("Use Tomelt.Conditions.Services.IConditionManager instead.")]
    public interface IRuleManager : IDependency {
        bool Matches(string expression);
    }
}