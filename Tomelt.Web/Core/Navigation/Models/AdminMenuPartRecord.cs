using System.ComponentModel.DataAnnotations;
using Tomelt.ContentManagement.Records;

namespace Tomelt.Core.Navigation.Models {
    public class AdminMenuPartRecord : ContentPartRecord {
        public const ushort DefaultMenuTextLength = 255;

        [StringLength(DefaultMenuTextLength)]
        public virtual string AdminMenuText { get; set; }
        public virtual string AdminMenuPosition { get; set; }
        public virtual bool OnAdminMenu { get; set; }
    }
}