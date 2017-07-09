using System.ComponentModel.DataAnnotations;
using Tomelt.ContentManagement;
using Tomelt.ContentManagement.Aspects;

namespace Tomelt.Core.Title.Models {
    public class TitlePart : ContentPart<TitlePartRecord>, ITitleAspect {
        [Required]
        public string Title {
            get { return Retrieve(x => x.Title); }
            set { Store(x => x.Title, value); }
        }
    }
}