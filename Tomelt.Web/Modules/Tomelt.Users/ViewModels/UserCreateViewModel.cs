using System.ComponentModel.DataAnnotations;
using Tomelt.ContentManagement;
using Tomelt.Users.Models;

namespace Tomelt.Users.ViewModels {
    public class UserCreateViewModel  {
        [Required]
        public string UserName { get; set; }

        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 7)]
        public string Password { get; set; }

        [Required, DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}