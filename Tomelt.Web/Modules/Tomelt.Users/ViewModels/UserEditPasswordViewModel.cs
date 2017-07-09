using Tomelt.ContentManagement;
using Tomelt.Environment.Extensions;
using System.ComponentModel.DataAnnotations;

namespace Tomelt.Users.ViewModels
{
    [TomeltFeature("Tomelt.Users.EditPasswordByAdmin")]
    public class UserEditPasswordViewModel {
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 7)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        public IContent User { get; set; }
    }
}