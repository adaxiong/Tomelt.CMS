using Tomelt.Security;

namespace Tomelt.Roles.Events {
    public class UserRoleContext : RoleContext {
        public IUser User { get; set; }
    }
}