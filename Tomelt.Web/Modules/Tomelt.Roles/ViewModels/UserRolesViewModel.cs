using System.Collections.Generic;
using Tomelt.Roles.Models;
using Tomelt.Security;

namespace Tomelt.Roles.ViewModels {
    public class UserRolesViewModel {
        public UserRolesViewModel() {
            Roles = new List<UserRoleEntry>();
        }

        public IUser User { get; set; }
        public IUserRoles UserRoles { get; set; }
        public IList<UserRoleEntry> Roles { get; set; }
    }

    public class UserRoleEntry {
        public int RoleId { get; set; }
        public string Name { get; set; }
        public bool Granted { get; set; }
    }
}
