using System.ComponentModel.DataAnnotations;
using Tomelt.Layouts.Framework.Elements;

namespace Tomelt.Layouts.ViewModels {
    public class ElementBlueprintPropertiesViewModel {
        public string ElementTypeName { get; set; }

        [Required]
        public string ElementDisplayName { get; set; }

        [MaxLength(2048)]
        public string ElementDescription { get; set; }

        public string ElementCategory { get; set; }

        public Element BaseElement { get; set; }
    }
}