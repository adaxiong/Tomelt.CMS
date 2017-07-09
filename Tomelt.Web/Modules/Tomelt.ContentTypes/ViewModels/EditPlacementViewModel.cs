using System.Collections.Generic;
using Tomelt.ContentManagement.MetaData.Models;
using Tomelt.ContentTypes.Services;
using Tomelt.ContentTypes.Settings;

namespace Tomelt.ContentTypes.ViewModels {
    public class EditPlacementViewModel {
        public ContentTypeDefinition ContentTypeDefinition { get; set; }
        public PlacementSettings[] PlacementSettings { get; set; }
        public List<DriverResultPlacement> AllPlacements { get; set; }
    }
}