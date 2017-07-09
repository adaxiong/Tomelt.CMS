using System.Collections.Generic;
using Tomelt.Services;

namespace Tomelt.Layouts.Services {
    public interface IElementFilterProcessor : IDependency {
        string ProcessContent(string text, string flavor, IDictionary<string, object> context);
    }
}