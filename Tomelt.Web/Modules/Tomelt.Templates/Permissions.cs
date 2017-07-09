using System.Collections.Generic;
using Tomelt.Environment.Extensions.Models;
using Tomelt.Security.Permissions;

namespace Tomelt.Templates {
    public class Permissions : IPermissionProvider {
        public static readonly Permission ManageTemplates = new Permission { Description = "Managing Templates", Name = "ManageTemplates" };

        public virtual Feature Feature { get; set; }

        public IEnumerable<Permission> GetPermissions() {
            return new[] {
                ManageTemplates,
            };
        }

        public IEnumerable<PermissionStereotype> GetDefaultStereotypes() {
            return new[] {
                new PermissionStereotype {
                    Name = "Administrator",
                    Permissions = new[] { ManageTemplates }
                },
                new PermissionStereotype {
                    Name = "Editor",
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