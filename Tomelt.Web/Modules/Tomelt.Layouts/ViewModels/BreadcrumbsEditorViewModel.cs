using System.Collections.Generic;
using Tomelt.ContentManagement;

namespace Tomelt.Layouts.ViewModels {
    public class BreadcrumbsEditorViewModel {
        public IEnumerable<ContentItem> Menus { get; set; }
        public int CurrentMenuId { get; set; }

        public int StartLevel { get; set; }
        public int StopLevel { get; set; }
        public bool AddHomePage { get; set; }
        public bool AddCurrentPage { get; set; }
    }
}