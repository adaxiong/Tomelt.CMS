using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tomelt.Tags.ViewModels {
    public class TagsAdminCreateViewModel {
        [Required, DisplayName("Name")]
        public string TagName { get; set; }
    }
}
