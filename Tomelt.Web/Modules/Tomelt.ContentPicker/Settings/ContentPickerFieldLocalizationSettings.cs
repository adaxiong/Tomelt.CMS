using Tomelt.Environment.Extensions;

namespace Tomelt.ContentPicker.Settings {
    [TomeltFeature("Tomelt.ContentPicker.LocalizationExtensions")]
    public class ContentPickerFieldLocalizationSettings {

        public ContentPickerFieldLocalizationSettings() {
            TryToLocalizeItems = true;
        }
        public bool TryToLocalizeItems { get; set; }
        public bool RemoveItemsWithoutLocalization { get; set; }
        public bool RemoveItemsWithNoLocalizationPart { get; set; }
        public bool AssertItemsHaveSameCulture { get; set; }
        public bool BlockForItemsWithNoLocalizationPart { get; set; }
    }
}