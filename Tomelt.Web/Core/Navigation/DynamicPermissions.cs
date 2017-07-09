using System;
using System.Collections.Generic;
using System.Linq;
using Tomelt.ContentManagement;
using Tomelt.ContentManagement.MetaData.Models;
using Tomelt.Core.Navigation.Services;
using Tomelt.Core.Title.Models;
using Tomelt.Environment.Extensions.Models;
using Tomelt.Security.Permissions;

namespace Tomelt.Core.Navigation {
    public class DynamicPermissions : IPermissionProvider {
        private static readonly Permission ManageMenu = new Permission { Description = "Manage '{0}' menu", Name = "Manage_{0}", ImpliedBy = new[] { Permissions.ManageMenus } };

        private readonly IMenuService _menuService;
        private readonly IContentManager _contentManager;

        public virtual Feature Feature { get; set; }

        public DynamicPermissions(
            IMenuService menuService,
            IContentManager contentManager) {
            _menuService = menuService;
            _contentManager = contentManager;
        }

        public IEnumerable<Permission> GetPermissions() {
            return _menuService.GetMenus().Select(menu => CreateMenuPermission(menu, _contentManager));
        }

        public IEnumerable<PermissionStereotype> GetDefaultStereotypes() {
            return Enumerable.Empty<PermissionStereotype>();
        }

        public static Permission CreateMenuPermission(ContentItem menu, IContentManager contentManager) {
            var metadata = contentManager.GetItemMetadata(menu);

            return new Permission {
                Name = String.Format(ManageMenu.Name, metadata.Identity),
                Description = String.Format(ManageMenu.Description, metadata.DisplayText),
                Category = "Navigation Feature",
                ImpliedBy = new[] { Permissions.ManageMenus }
            };
        }
    }
}
