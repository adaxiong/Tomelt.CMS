using System.Collections.Generic;
using Tomelt.Recipes.Models;

namespace Tomelt.Recipes.ViewModels {
    public class ContentBuilderStepViewModel {
        public ContentBuilderStepViewModel() {
            ContentTypes = new List<ContentTypeEntry>();
        }

        public IList<ContentTypeEntry> ContentTypes { get; set; }
        public VersionHistoryOptions VersionHistoryOptions { get; set; }
    }
}