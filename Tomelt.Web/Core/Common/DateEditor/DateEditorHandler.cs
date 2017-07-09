using Tomelt.Core.Common.Models;
using Tomelt.ContentManagement.Handlers;
using Tomelt.Core.Common.Utilities;

namespace Tomelt.Core.Common.DateEditor {
    public class DateEditorHandler : ContentHandler {
        public DateEditorHandler() {
            OnPublished<CommonPart>((context, part) => {
                var settings = part.TypePartDefinition.Settings.GetModel<DateEditorSettings>();
                if (!settings.ShowDateEditor) {
                    return;
                }

                var thisIsTheInitialVersionRecord = part.ContentItem.Version < 2;
                var theDatesHaveNotBeenModified = DateUtils.DatesAreEquivalent(part.CreatedUtc, part.VersionCreatedUtc);
                var theContentDateShouldBeUpdated = thisIsTheInitialVersionRecord && theDatesHaveNotBeenModified;

                if (theContentDateShouldBeUpdated) {
                    // "touch" CreatedUtc in ContentItemRecord
                    part.CreatedUtc = part.PublishedUtc;
                }
            });
        }
    }
}
