using System.Collections.Generic;
using Tomelt.Environment.Extensions.Models;
using Tomelt.Security.Permissions;

namespace Tomelt.Themes {
    public class Permissions : IPermissionProvider {
        public static readonly Permission ApplyTheme = new Permission { Description = "Apply a Theme", Name = "ApplyTheme" };

        public virtual Feature Feature { get; set; }

        public IEnumerable<Permission> GetPermissions() {
            return new[] {
                                        ApplyTheme,
                                    };
        }

        public IEnumerable<PermissionStereotype> GetDefaultStereotypes() {
            return new[] {
                             new PermissionStereotype {
                                                          Name = "Administrator",
                                                          Permissions = new[] {ApplyTheme}
                                                      },
                         };
        }
    }
}