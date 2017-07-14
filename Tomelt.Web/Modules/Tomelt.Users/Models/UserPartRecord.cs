using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Security;
using Tomelt.ContentManagement.Records;

namespace Tomelt.Users.Models {
    public class UserPartRecord : ContentPartRecord {
        [Display(Name = "用户名")]
        public virtual string UserName { get; set; }
        [Display(Name = "电子邮箱")]
        public virtual string Email { get; set; }
        public virtual string NormalizedUserName { get; set; }

        public virtual string Password { get; set; }
        public virtual MembershipPasswordFormat PasswordFormat { get; set; }
        public virtual string HashAlgorithm { get; set; }
        public virtual string PasswordSalt { get; set; }

        public virtual UserStatus RegistrationStatus { get; set; }
        public virtual UserStatus EmailStatus { get; set; }
        public virtual string EmailChallengeToken { get; set; }
        public virtual DateTime? CreatedUtc { get; set; }
        public virtual DateTime? LastLoginUtc { get; set; }
        public virtual DateTime? LastLogoutUtc { get; set; }
    }
}