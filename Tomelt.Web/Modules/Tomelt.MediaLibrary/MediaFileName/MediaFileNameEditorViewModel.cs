using System.ComponentModel.DataAnnotations;
using Tomelt.DisplayManagement.Shapes;

namespace Tomelt.MediaLibrary.MediaFileName
{
    public class MediaFileNameEditorViewModel : Shape {
        [Required]
        public string FileName { get; set; }
    }
}