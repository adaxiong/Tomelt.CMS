using Tomelt.Roles.Models;

namespace Tomelt.Roles.Events {
    public class PermissionRoleContext : RoleContext {
        public PermissionRecord Permission { get; set; }
    }
}