using System.Collections.Generic;
using Tomelt.Environment.Extensions.Models;
using Tomelt.Security.Permissions;

namespace Tomelt.Widgets {
    public class Permissions : IPermissionProvider {
        public static readonly Permission ManageWidgets = new Permission { Description = "Managing Widgets", Name = "ManageWidgets" };

        public virtual Feature Feature { get; set; }

        public IEnumerable<Permission> GetPermissions() {
            return new[] {
                ManageWidgets,
            };
        }

        public IEnumerable<PermissionStereotype> GetDefaultStereotypes() {
            return new[] {
                new PermissionStereotype {
                    Name = "Administrator",
                    Permissions = new[] {ManageWidgets}
                },
            };
        }

    }
}