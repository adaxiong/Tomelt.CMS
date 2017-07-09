﻿using Tomelt.ContentManagement;
using Tomelt.ContentManagement.Handlers;
using Tomelt.Core.Navigation.Services;
using Tomelt.Core.Title.Models;

namespace Tomelt.Core.Navigation.Handlers {
    public class MenuHandler : ContentHandler {
        private readonly IMenuService _menuService;
        private readonly IContentManager _contentManager;

        public MenuHandler(IMenuService menuService, IContentManager contentManager) {
            _menuService = menuService;
            _contentManager = contentManager;
        }

        protected override void Removing(RemoveContentContext context) {
            if (context.ContentItem.ContentType != "Menu") {
                return;
            }

            // remove all menu items
            var menuParts = _menuService.GetMenuParts(context.ContentItem.Id);

            foreach(var menuPart in menuParts) {
                _contentManager.Remove(menuPart.ContentItem);
            }
        }
    }
}