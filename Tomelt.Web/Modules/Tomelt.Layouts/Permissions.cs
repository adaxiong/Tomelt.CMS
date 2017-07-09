using System.Collections.Generic;
using Tomelt.Environment.Extensions.Models;
using Tomelt.Security.Permissions;

namespace Tomelt.Layouts {
    public class Permissions : IPermissionProvider {
        public static readonly Permission ManageLayouts = new Permission { Description = "Managing Layouts", Name = "ManageLayouts" };

        public virtual Feature Feature { get; set; }

        public IEnumerable<Permission> GetPermissions() {
            return new[] {
                ManageLayouts,
            };
        }

        public IEnumerable<PermissionStereotype> GetDefaultStereotypes() {
            return new[] {
                new PermissionStereotype {
                    Name = "Administrator",
                    Permissions = new[] { ManageLayouts }
                },
                new PermissionStereotype {
                    Name = "Editor",
                    Permissions = new[] { ManageLayouts }
                },
                new PermissionStereotype {
                    Name = "Moderator",
                },
                new PermissionStereotype {
                    Name = "Author"
                },
                new PermissionStereotype {
                    Name = "Contributor",
                },
            };
        }

    }
}