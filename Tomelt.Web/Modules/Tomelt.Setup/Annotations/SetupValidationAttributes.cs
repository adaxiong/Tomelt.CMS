using Tomelt.Localization;

namespace Tomelt.Setup.Annotations {
    public class SiteNameValidAttribute : System.ComponentModel.DataAnnotations.RangeAttribute {
        private string _value;

        public SiteNameValidAttribute(int maximumLength)
            : base(1, maximumLength) {
            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        public override bool IsValid(object value) {
            _value = (value as string) ?? "";
            return base.IsValid(_value.Trim().Length);
        }

        public override string FormatErrorMessage(string name) {
            if (string.IsNullOrWhiteSpace(_value))
                return T("站点名称必须填写.").Text;

            return T("站点名称长度不能超过 {0} 位.", Maximum).Text;
        }
    }

    public class UserNameValidAttribute : System.ComponentModel.DataAnnotations.RangeAttribute {
        private string _value;

        public UserNameValidAttribute(int minimumLength, int maximumLength)
            : base(minimumLength, maximumLength) {
            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        public override bool IsValid(object value) {
            _value = (value as string) ?? "";
            return base.IsValid(_value.Trim().Length);
        }

        public override string FormatErrorMessage(string name) {
            if (string.IsNullOrEmpty(_value))
                return T("User name is required.").Text;

            return _value.Length < (int)Minimum
                ? T("用户名长度至少 {0} 位.", Minimum).Text
                : T("用户名长度不能超过 {0} 位.", Maximum).Text;
        }
    }

    public class PasswordValidAttribute : System.ComponentModel.DataAnnotations.RangeAttribute {
        private string _value;

        public PasswordValidAttribute(int minimumLength, int maximumLength)
            : base(minimumLength, maximumLength) {
            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        public override bool IsValid(object value) {
            _value = (value as string) ?? "";
            return base.IsValid(_value.Trim().Length);
        }

        public override string FormatErrorMessage(string name) {
            if (string.IsNullOrEmpty(_value))
                return T("密码必须填写.").Text;

            return _value.Length < (int)Minimum
                ? T("密码长度至少需 {0} 位.", Minimum).Text
                : T("密码长度不能超过 {0} 位.", Maximum).Text;
        }
    }

    public class PasswordConfirmationRequiredAttribute : System.ComponentModel.DataAnnotations.RequiredAttribute {
        public PasswordConfirmationRequiredAttribute() {
            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        public override string FormatErrorMessage(string name) {
            return T("密码必须填写.").Text;
        }
    }
}