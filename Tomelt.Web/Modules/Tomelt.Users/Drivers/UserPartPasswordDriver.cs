using Tomelt.ContentManagement;
using Tomelt.ContentManagement.Drivers;
using Tomelt.Environment.Extensions;
using Tomelt.Localization;
using Tomelt.Security;
using Tomelt.Users.Models;
using Tomelt.Users.ViewModels;

namespace Tomelt.Users.Drivers{

    [TomeltFeature("Tomelt.Users.PasswordEditor")]
    public class UserPartPasswordDriver : ContentPartDriver<UserPart> {
        private readonly IMembershipService _membershipService;
        
        public Localizer T { get; set; }
        
        public UserPartPasswordDriver(IMembershipService membershipService) {
            _membershipService = membershipService;
            T = NullLocalizer.Instance;
        }

        protected override DriverResult Editor(UserPart part, dynamic shapeHelper) {
            return ContentShape("Parts_User_EditPassword_Edit",
                () => shapeHelper.EditorTemplate(
                    TemplateName: "Parts/User.EditPassword",
                    Model: new UserEditPasswordViewModel { User = part },
                    Prefix: Prefix));
        }

        protected override DriverResult Editor(UserPart part, IUpdateModel updater, dynamic shapeHelper) {
            var editModel = new UserEditPasswordViewModel { User = part };
            if (updater != null) {               
                if (updater.TryUpdateModel(editModel,Prefix,null,null)) {
                    if (!(string.IsNullOrEmpty(editModel.Password) && string.IsNullOrEmpty(editModel.ConfirmPassword))) {
                        if (string.IsNullOrEmpty(editModel.Password) || string.IsNullOrEmpty(editModel.ConfirmPassword)) {
                            updater.AddModelError("MissingPassword", T("Password or Confirm Password field is empty."));
                        }
                        else {
                            if (editModel.Password != editModel.ConfirmPassword){
                                updater.AddModelError("ConfirmPassword", T("Password confirmation must match."));
                            }
                            var actUser = _membershipService.GetUser(part.UserName);
                            _membershipService.SetPassword(actUser, editModel.Password);
                        }
                    }
                }
            }
            return Editor(part, shapeHelper);
        }  
    }
}