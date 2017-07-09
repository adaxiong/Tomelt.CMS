using System.Collections.Generic;
using Tomelt.Environment.Extensions.Models;

namespace Tomelt.Themes.Services {
    public interface IThemeService : IDependency {
        void DisableThemeFeatures(string themeName);
        void EnableThemeFeatures(string themeName);
        bool IsRecentlyInstalled(ExtensionDescriptor module);
        void DisablePreviewFeatures(IEnumerable<string> features);
    }
}