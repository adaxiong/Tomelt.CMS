using Tomelt.ContentManagement;

namespace Tomelt.Core.Navigation.Models {
    public class ShapeMenuItemPart : ContentPart {
        /// <summary>
        /// Maximum number of items to retrieve from db
        /// </summary>
        public string ShapeType {
            get { return this.Retrieve(x => x.ShapeType); }
            set { this.Store(x => x.ShapeType, value); }
        }
    }
}