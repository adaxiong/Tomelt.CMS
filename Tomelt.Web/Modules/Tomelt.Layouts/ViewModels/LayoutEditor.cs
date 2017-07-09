using System.Collections.Generic;
using Tomelt.ContentManagement;
using Tomelt.Layouts.Models;

namespace Tomelt.Layouts.ViewModels {
    public class LayoutEditor {
        public IContent Content { get; set; }
        public string Data { get; set; }
        public string ConfigurationData { get; set; }
        public string RecycleBin { get; set; }
        public int? TemplateId { get; set; }
        public string SessionKey { get; set; }
        public IList<LayoutPart> Templates { get; set; }
    }
}