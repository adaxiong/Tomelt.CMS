using Tomelt.Core.Navigation.Models;
using Tomelt.ContentManagement.Handlers;

namespace Tomelt.Core.Navigation.Handlers {
    public class MenuItemPartHandler : ContentHandler {
        public MenuItemPartHandler() {
            Filters.Add(new ActivatingFilter<MenuItemPart>("MenuItem"));
        }
    }
}