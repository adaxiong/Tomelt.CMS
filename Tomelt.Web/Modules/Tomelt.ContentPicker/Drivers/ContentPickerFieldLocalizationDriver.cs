using System.Linq;
using Tomelt.ContentManagement;
using Tomelt.ContentManagement.Drivers;
using Tomelt.ContentPicker.Fields;
using Tomelt.ContentPicker.ViewModels;
using Tomelt.Environment.Extensions;

namespace Tomelt.ContentPicker.Drivers {
    [TomeltFeature("Tomelt.ContentPicker.LocalizationExtensions")]
    public class ContentPickerFieldLocalizationDriver : ContentFieldDriver<Fields.ContentPickerField> {
        private readonly IContentManager _contentManager;

        public ContentPickerFieldLocalizationDriver(IContentManager contentManager) {
            _contentManager = contentManager;
        }

        private static string GetPrefix(Fields.ContentPickerField field, ContentPart part) {
            return part.PartDefinition.Name + "." + field.Name;
        }

        private static string GetDifferentiator(Fields.ContentPickerField field, ContentPart part) {
            return field.Name;
        }

        protected override DriverResult Editor(ContentPart part, Fields.ContentPickerField field, dynamic shapeHelper) {
            return ContentShape("Fields_ContentPickerLocalization_Edit", GetDifferentiator(field, part),
                () => {
                    var model = new ContentPickerFieldViewModel {
                        Field = field,
                        Part = part,
                        ContentItems = _contentManager.GetMany<ContentItem>(field.Ids, VersionOptions.Latest, QueryHints.Empty).ToList()
                    };

                    model.SelectedIds = string.Join(",", field.Ids);

                    return shapeHelper.EditorTemplate(TemplateName: "Fields/ContentPickerLocalization.Edit", Model: model, Prefix: GetPrefix(field, part));
                });
        }

        protected override DriverResult Editor(ContentPart part, ContentPickerField field, IUpdateModel updater, dynamic shapeHelper) {
            return Editor(part, field, shapeHelper);
        }
    }
}