using System.ComponentModel.DataAnnotations;
using Tomelt.ContentManagement.Records;

namespace Tomelt.Core.Title.Models {
    public class TitlePartRecord : ContentPartVersionRecord {
        [StringLength(1024)]
        public virtual string Title { get; set; }
    }
}
