using System.ComponentModel.DataAnnotations;

namespace Tomelt.MediaProcessing.ViewModels {
    public class AdminCreateViewModel {
        [Required, StringLength(1024)]
        public string Name { get; set; }
    }
}