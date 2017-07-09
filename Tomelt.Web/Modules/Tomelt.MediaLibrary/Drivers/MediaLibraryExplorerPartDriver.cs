using Tomelt.ContentManagement.Drivers;
using Tomelt.MediaLibrary.Models;

namespace Tomelt.MediaLibrary.Drivers {
    public class MediaLibraryExplorerPartDriver : ContentPartDriver<MediaLibraryExplorerPart> {
        protected override DriverResult Display(MediaLibraryExplorerPart part, string displayType, dynamic shapeHelper) {
            return Combined(
                ContentShape("Parts_MediaLibrary_Navigation", () => shapeHelper.Parts_MediaLibrary_Navigation()),
                ContentShape("Parts_MediaLibrary_Actions", () => shapeHelper.Parts_MediaLibrary_Actions())
            );
        }
    }
}