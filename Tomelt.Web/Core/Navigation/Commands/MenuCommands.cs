using System;
using Tomelt.Commands;
using Tomelt.ContentManagement;
using Tomelt.ContentManagement.Aspects;
using Tomelt.Core.Navigation.Models;
using Tomelt.Core.Navigation.Services;
using Tomelt.Security;
using Tomelt.Settings;

namespace Tomelt.Core.Navigation.Commands {
    public class MenuCommands : DefaultTomeltCommandHandler
    {
        private readonly IContentManager _contentManager;
        private readonly IMenuService _menuService;
        private readonly ISiteService _siteService;
        private readonly IMembershipService _membershipService;

        public MenuCommands(
            IContentManager contentManager, 
            IMenuService menuService,
            ISiteService siteService,
            IMembershipService membershipService) {
            _contentManager = contentManager;
            _menuService = menuService;
            _siteService = siteService;
            _membershipService = membershipService;
        }

        [TomeltSwitch]
        public string MenuPosition { get; set; }
        
        [TomeltSwitch]
        public string Owner { get; set; }

        [TomeltSwitch]
        public string MenuText { get; set; }

        [TomeltSwitch]
        public string Url { get; set; }

        [TomeltSwitch]
        public string MenuName { get; set; }

        [CommandName("menuitem create")]
        [CommandHelp("menuitem create /MenuPosition:<position> /MenuText:<text> /Url:<url> /MenuName:<name> [/Owner:<username>] \r\n\t" + "Creates a new menu item")]
        [TomeltSwitches("MenuPosition,MenuText,Url,MenuName,Owner")]
        public void Create() {
            // flushes before doing a query in case a previous command created the menu

            var menu = _menuService.GetMenu(MenuName);

            if(menu == null) {
                Context.Output.WriteLine(T("Menu not found.").Text);
                return;
            }

            var menuItem = _contentManager.Create("MenuItem");
            menuItem.As<MenuPart>().MenuPosition = MenuPosition;
            menuItem.As<MenuPart>().MenuText = T(MenuText).ToString();
            menuItem.As<MenuPart>().Menu = menu.ContentItem;
            menuItem.As<MenuItemPart>().Url = Url;

            if (String.IsNullOrEmpty(Owner)) {
                Owner = _siteService.GetSiteSettings().SuperUser;
            }
            var owner = _membershipService.GetUser(Owner);

            if (owner == null) {
                Context.Output.WriteLine(T("Invalid username: {0}", Owner));
                return;
            }

            menuItem.As<ICommonPart>().Owner = owner;

            Context.Output.WriteLine(T("Menu item created successfully.").Text);
        }

        [CommandName("menu create")]
        [CommandHelp("menu create /MenuName:<name>\r\n\t" + "Creates a new menu")]
        [TomeltSwitches("MenuName")]
        public void CreateMenu() {
            if (string.IsNullOrWhiteSpace(MenuName)) {
                Context.Output.WriteLine(T("Menu name can't be empty.").Text);
                return;
            }

            _menuService.Create(MenuName);

            Context.Output.WriteLine(T("Menu created successfully.").Text);
        }
    }
}