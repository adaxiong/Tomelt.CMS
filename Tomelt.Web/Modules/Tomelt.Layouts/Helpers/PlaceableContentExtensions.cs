using Tomelt.ContentManagement;
using Tomelt.ContentManagement.FieldStorage.InfosetStorage;

namespace Tomelt.Layouts.Helpers {
    public static class PlaceableContentExtensions {
        public static bool IsPlaceableContent(this IContent content) {
            return content.As<InfosetPart>().Retrieve<bool>("PlacedAsElement");
        }

        public static void IsPlaceableContent(this IContent content, bool value) {
            content.As<InfosetPart>().Store("PlacedAsElement", value);
        }
    }
}