using System.ComponentModel.DataAnnotations;
using Tomelt.DisplayManagement.Shapes;

namespace Tomelt.Core.Common.OwnerEditor {
    public class OwnerEditorViewModel : Shape {
        [Required]
        public string Owner { get; set; }
    }
}