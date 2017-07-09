using System.Linq;
using Tomelt.ContentManagement;
using Tomelt.ContentManagement.Drivers;
using Tomelt.ContentPicker.Models;
using Tomelt.ContentPicker.ViewModels;
using Tomelt.Core.Navigation;
using Tomelt.Core.Navigation.Models;
using Tomelt.Core.Navigation.Services;
using Tomelt.Core.Navigation.ViewModels;
using Tomelt.Localization;
using Tomelt.Security;
using Tomelt.UI.Navigation;
using Tomelt.Utility;

namespace Tomelt.ContentPicker.Drivers {
    
    public class NavigationPartDriver : ContentPartDriver<NavigationPart> {
        private readonly IAuthorizationService _authorizationService;
        private readonly IWorkContextAccessor _workContextAccessor;
        private readonly IContentManager _contentManager;
        private readonly IMenuService _menuService;
        private readonly INavigationManager _navigationManager;

        public NavigationPartDriver(
            IAuthorizationService authorizationService, 
            IWorkContextAccessor workContextAccessor,
            IContentManager contentManager,
            IMenuService menuService,
            INavigationManager navigationManager) {
            _authorizationService = authorizationService;
            _workContextAccessor = workContextAccessor;
            _contentManager = contentManager;
            _menuService = menuService;
            _navigationManager = navigationManager;

            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        protected override string Prefix {
            get {
                return "NavigationPart";
            }
        }

        protected override DriverResult Editor(NavigationPart part, dynamic shapeHelper) {
            var currentUser = _workContextAccessor.GetContext().CurrentUser;
            var allowedMenus = _menuService.GetMenus().Where(menu => _authorizationService.TryCheckAccess(Permissions.ManageMenus, currentUser, menu)).ToList();

            if (!allowedMenus.Any())
                return null;

            return ContentShape("Parts_Navigation_Edit",
                                () => {
                                    // loads all menu part of type ContentMenuItem linking to the current content item
                                    var model = new NavigationPartViewModel {
                                        Part = part,
                                        ContentMenuItems = _contentManager
                                            .Query<MenuPart>()
                                            .Join<ContentMenuItemPartRecord>()
                                            .Where(x => x.ContentMenuItemRecord == part.ContentItem.Record)
                                            .List(),
                                        Menus = allowedMenus,
                                    };

                                    return shapeHelper.EditorTemplate(TemplateName: "Parts.Navigation.Edit", Model: model, Prefix: Prefix);
                                });
        }

        protected override DriverResult Editor(NavigationPart part, IUpdateModel updater, dynamic shapeHelper) {
            var currentUser = _workContextAccessor.GetContext().CurrentUser;
            var allowedMenus = _menuService.GetMenus().Where(menu => _authorizationService.TryCheckAccess(Permissions.ManageMenus, currentUser, menu)).ToList();

            if (!allowedMenus.Any())
                return null;

            var model = new NavigationPartViewModel();

            if (updater.TryUpdateModel(model, Prefix, null, null)) {
                if(model.AddMenuItem) {
                    if (string.IsNullOrEmpty(model.MenuText)) {
                        updater.AddModelError("MenuText", T("The MenuText field is required"));
                    }
                    else {
                        var menu = allowedMenus.FirstOrDefault(m => m.Id == model.CurrentMenuId);

                        if(menu != null) {
                            var menuItem = _contentManager.Create<ContentMenuItemPart>("ContentMenuItem");
                            menuItem.Content = part.ContentItem;

                            menuItem.As<MenuPart>().MenuText = model.MenuText;
                            menuItem.As<MenuPart>().MenuPosition = Position.GetNext(_navigationManager.BuildMenu(menu));
                            menuItem.As<MenuPart>().Menu = menu;
                        }
                    }
                }
            }

            return Editor(part, shapeHelper);
        }
    }
}