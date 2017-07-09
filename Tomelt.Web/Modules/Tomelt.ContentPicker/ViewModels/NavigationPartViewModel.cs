using System.Collections.Generic;
using Tomelt.ContentManagement;
using Tomelt.ContentPicker.Models;
using Tomelt.Core.Navigation.Models;

namespace Tomelt.ContentPicker.ViewModels {
    public class NavigationPartViewModel {
        public IEnumerable<MenuPart> ContentMenuItems { get; set; }
        public NavigationPart Part { get; set; }
        public IEnumerable<ContentItem> Menus { get; set; }
        public string MenuText { get; set; }
        public bool AddMenuItem { get; set; }
        public int CurrentMenuId { get; set; }
    }
}