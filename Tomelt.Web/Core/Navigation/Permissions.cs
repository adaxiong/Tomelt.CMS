using System.Collections.Generic;
using Tomelt.Environment.Extensions.Models;
using Tomelt.Security.Permissions;

namespace Tomelt.Core.Navigation {
    public class Permissions : IPermissionProvider {
        public static readonly Permission ManageMenus = new Permission { Name = "ManageMenus", Description = "Manage all menus" };

        public virtual Feature Feature { get; set; }

        public IEnumerable<Permission> GetPermissions() {
            return new[] {
                ManageMenus
             };
        }

        public IEnumerable<PermissionStereotype> GetDefaultStereotypes() {
            return new[] {
                new PermissionStereotype {
                    Name = "Administrator",
                    Permissions = new[] {ManageMenus}
                }
            };
        }
    }
}
