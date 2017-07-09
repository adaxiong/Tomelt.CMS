using System.Collections.Generic;

namespace Tomelt.Localization.Services {
    public interface ILocalizationStreamParser : IDependency {
        void ParseLocalizationStream(string text, IDictionary<string, string> translations, bool merge);
    }
}