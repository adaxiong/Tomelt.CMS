using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Tomelt.MediaLibrary.Models;

namespace Tomelt.MediaLibrary.ViewModels {
    public class MediaManagerFolderEditViewModel {
        [Required]
        public string Name { get; set; }
        public string FolderPath { get; set; }
    }
}
