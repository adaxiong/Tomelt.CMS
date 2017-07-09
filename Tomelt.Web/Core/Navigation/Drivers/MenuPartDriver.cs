using System.Linq;
using Tomelt.ContentManagement;
using Tomelt.ContentManagement.Drivers;
using Tomelt.Core.Navigation.Models;
using Tomelt.Core.Navigation.Services;
using Tomelt.Core.Navigation.ViewModels;
using Tomelt.Localization;
using Tomelt.Security;
using Tomelt.UI.Navigation;
using Tomelt.Utility;

namespace Tomelt.Core.Navigation.Drivers {
    public class MenuPartDriver : ContentPartDriver<MenuPart> {
        private readonly IAuthorizationService _authorizationService;
        private readonly INavigationManager _navigationManager;
        private readonly ITomeltServices _tomeltServices;
        private readonly IMenuService _menuService;

        public MenuPartDriver(
            IAuthorizationService authorizationService, 
            INavigationManager navigationManager, 
            ITomeltServices tomeltServices,
            IMenuService menuService) {
            _authorizationService = authorizationService;
            _navigationManager = navigationManager;
            _tomeltServices = tomeltServices;
            _menuService = menuService;
            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        protected override string Prefix {
            get {
                return "MenuPart";
            }
        }

        protected override DriverResult Editor(MenuPart part, dynamic shapeHelper) {
            var allowedMenus = _menuService.GetMenus().Where(menu => _authorizationService.TryCheckAccess(Permissions.ManageMenus, _tomeltServices.WorkContext.CurrentUser, menu)).ToList();

            if (!allowedMenus.Any())
                return null;

            return ContentShape("Parts_Navigation_Menu_Edit", () => {
                var model = new MenuPartViewModel {
                    CurrentMenuId = part.Menu == null ? -1 : part.Menu.Id,
                    ContentItem = part.ContentItem,
                    Menus = allowedMenus,
                    OnMenu = part.Menu != null,
                    MenuText = part.MenuText
                };

                return shapeHelper.EditorTemplate(TemplateName: "Parts.Navigation.Menu.Edit", Model: model, Prefix: Prefix);
            });
        }

        protected override DriverResult Editor(MenuPart part, IUpdateModel updater, dynamic shapeHelper) {
            var model = new MenuPartViewModel();

            if(updater.TryUpdateModel(model, Prefix, null, null)) {
                var menu = model.OnMenu ? _tomeltServices.ContentManager.Get(model.CurrentMenuId) : null;

                if (!_authorizationService.TryCheckAccess(Permissions.ManageMenus, _tomeltServices.WorkContext.CurrentUser, menu))
                    return null;

                part.MenuText = model.MenuText;
                part.Menu = menu;

                if (string.IsNullOrEmpty(part.MenuPosition) && menu != null) {
                    part.MenuPosition = Position.GetNext(_navigationManager.BuildMenu(menu));

                    if (string.IsNullOrEmpty(part.MenuText)) {
                        updater.AddModelError("MenuText", T("The MenuText field is required"));
                    }
                }
            }

            return Editor(part, shapeHelper);
        }

        protected override void Importing(MenuPart part, ContentManagement.Handlers.ImportContentContext context) {
            // Don't do anything if the tag is not specified.
            if (context.Data.Element(part.PartDefinition.Name) == null) {
                return;
            }

            context.ImportAttribute(part.PartDefinition.Name, "MenuText", menuText =>
                part.MenuText = menuText
            );

            context.ImportAttribute(part.PartDefinition.Name, "MenuPosition", position =>
                part.MenuPosition = position
            );

            context.ImportAttribute(part.PartDefinition.Name, "Menu", menuIdentity => {
                var menu = context.GetItemFromSession(menuIdentity);
                if (menu != null) {
                    part.Menu = menu;
                }
            });
        }

        protected override void Exporting(MenuPart part, ContentManagement.Handlers.ExportContentContext context) {
            // is it on a menu ?
            if(part.Menu == null) {
                return;
            }

            var menu = _tomeltServices.ContentManager.Get(part.Menu.Id);
            var menuIdentity = _tomeltServices.ContentManager.GetItemMetadata(menu).Identity;
            context.Element(part.PartDefinition.Name).SetAttributeValue("Menu", menuIdentity);

            context.Element(part.PartDefinition.Name).SetAttributeValue("MenuText", part.MenuText);
            context.Element(part.PartDefinition.Name).SetAttributeValue("MenuPosition", part.MenuPosition);
        }
    }
}