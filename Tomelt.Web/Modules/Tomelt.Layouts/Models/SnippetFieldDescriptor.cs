using Tomelt.Localization;

namespace Tomelt.Layouts.Models {
    public class SnippetFieldDescriptor {
        public string Type { get; set; }
        public string Name { get; set; }
        public LocalizedString DisplayName { get; set; }
        public LocalizedString Description { get; set; }
    }
}