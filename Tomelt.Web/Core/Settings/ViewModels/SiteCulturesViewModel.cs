using System.Collections.Generic;

namespace Tomelt.Core.Settings.ViewModels {
    public class SiteCulturesViewModel  {
        public string CurrentCulture { get; set; }
        public IEnumerable<string> SiteCultures { get; set; }
        public IEnumerable<string> AvailableSystemCultures { get; set; }
    }
}