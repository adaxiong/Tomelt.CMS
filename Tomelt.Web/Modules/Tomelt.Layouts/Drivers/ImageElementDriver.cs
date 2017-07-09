using System;
using System.Linq;
using Tomelt.ContentManagement;
using Tomelt.Layouts.Elements;
using Tomelt.Layouts.Framework.Display;
using Tomelt.Layouts.Framework.Drivers;
using Tomelt.Layouts.Helpers;
using Tomelt.Layouts.ViewModels;
using Tomelt.MediaLibrary.Models;
using ContentItem = Tomelt.Layouts.Elements.ContentItem;

namespace Tomelt.Layouts.Drivers {
    public class ImageElementDriver : ElementDriver<Image> {
        private readonly IContentManager _contentManager;

        public ImageElementDriver(IContentManager contentManager) {
            _contentManager = contentManager;
        }

        protected override EditorResult OnBuildEditor(Image element, ElementEditorContext context) {

            var viewModel = new ImageEditorViewModel();
            var editor = context.ShapeFactory.EditorTemplate(TemplateName: "Elements.Image", Model: viewModel);

            if (context.Updater != null) {
                context.Updater.TryUpdateModel(viewModel, context.Prefix, null, null);
                element.MediaId = ParseImageId(viewModel.ImageId);
            }

            var imageId = element.MediaId;
            viewModel.CurrentImage = imageId != null ? GetImage(imageId.Value) : default(ImagePart);

            return Editor(context, editor);
        }

        protected override void OnDisplaying(Image element, ElementDisplayingContext context) {
            var imageId = element.MediaId;
            var image = imageId != null ? GetImage(imageId.Value) : default(ImagePart);
            context.ElementShape.ImagePart = image;
        }

        protected override void OnExporting(Image element, ExportElementContext context) {
            var image = element.MediaId != null ? GetImage(element.MediaId.Value) : default(ImagePart);

            if (image == null)
                return;

            context.ExportableData["Image"] = _contentManager.GetItemMetadata(image).Identity.ToString();
        }

        protected override void OnImporting(Image element, ImportElementContext context) {
            var imageIdentity = context.ExportableData.Get("Image");
            var image = !String.IsNullOrWhiteSpace(imageIdentity) ? context.Session.GetItemFromSession(imageIdentity) : default(ContentManagement.ContentItem);

            element.MediaId = image != null ? image.Id : default(int?);
        }

        protected ImagePart GetImage(int id) {
            return _contentManager.Get<ImagePart>(id, VersionOptions.Published);
        }

        private static int? ParseImageId(string imageId) {
            return ContentItem.Deserialize(imageId).FirstOrDefault();
        }
    }
}