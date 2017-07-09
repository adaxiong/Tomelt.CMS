using System.Collections.Generic;
using Tomelt.ContentManagement;
using Tomelt.ContentManagement.Utilities;

namespace Tomelt.Roles.Models {
    public class UserRolesPart : ContentPart, IUserRoles {

        internal LazyField<IList<string>> _roles = new LazyField<IList<string>>();

        public IList<string> Roles {
            get { return _roles.Value; }
        }
    }
}