using System.Collections.Generic;
using Tomelt.Services;

namespace Tomelt.Layouts.Services {
    public interface IElementFilter : IHtmlFilter {
        string ProcessContent(string text, string flavor, IDictionary<string, object> context);
    }
}