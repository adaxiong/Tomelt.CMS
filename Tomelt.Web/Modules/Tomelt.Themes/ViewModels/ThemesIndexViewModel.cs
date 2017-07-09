using System.Collections.Generic;
using Tomelt.Themes.Models;

namespace Tomelt.Themes.ViewModels {
    public class ThemesIndexViewModel {
        public bool InstallThemes { get; set; }
        public ThemeEntry CurrentTheme { get; set; }
        public IEnumerable<ThemeEntry> Themes { get; set; }
    }
}