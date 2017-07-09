using System.ComponentModel.DataAnnotations;

namespace Tomelt.Packaging.ViewModels {
    public class PackagingAddSourceViewModel {

        [Required]
        public string Url { get; set; }
    }
}