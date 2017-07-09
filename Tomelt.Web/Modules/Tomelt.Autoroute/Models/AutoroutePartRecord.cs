using Tomelt.ContentManagement.Records;
using System.ComponentModel.DataAnnotations;

namespace Tomelt.Autoroute.Models {
    public class AutoroutePartRecord : ContentPartVersionRecord {

        public virtual bool UseCustomPattern { get; set; }

        public virtual bool UseCulturePattern { get; set; }
        
        [StringLength(2048)]
        public virtual string CustomPattern { get; set; }
        
        [StringLength(2048)]
        public virtual string DisplayAlias { get; set; }
    }
}
