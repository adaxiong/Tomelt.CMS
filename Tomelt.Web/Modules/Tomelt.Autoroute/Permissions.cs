﻿using System.Collections.Generic;
using Tomelt.Environment.Extensions.Models;
using Tomelt.Security.Permissions;

namespace Tomelt.Autoroute {
    public class Permissions : IPermissionProvider {
        public static readonly Permission SetHomePage = new Permission { Description = "Set Home Page", Name = "SetHomePage" };

        public virtual Feature Feature { get; set; }

        public IEnumerable<Permission> GetPermissions() {
            return new[] {
                SetHomePage
            };
        }

        public IEnumerable<PermissionStereotype> GetDefaultStereotypes() {
            return new[] {
                new PermissionStereotype {
                    Name = "Administrator",
                    Permissions = new[] {SetHomePage}
                },
                new PermissionStereotype {
                    Name = "Editor",
                    Permissions = new[] {SetHomePage}
                }
            };
        }

    }
}