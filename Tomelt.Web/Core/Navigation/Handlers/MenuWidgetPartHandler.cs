using Tomelt.ContentManagement.Handlers;
using Tomelt.Core.Navigation.Models;

namespace Tomelt.Core.Navigation.Handlers {
    public class MenuWidgetPartHandler : ContentHandler {
        public MenuWidgetPartHandler() {
            OnInitializing<MenuWidgetPart>((context, part) => { part.StartLevel = 1; });
        }
    }
}