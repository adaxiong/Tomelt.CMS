using Tomelt.Layouts.Elements;
using Tomelt.Layouts.Framework.Display;
using Tomelt.Layouts.Framework.Drivers;
using Tomelt.Layouts.Helpers;
using Tomelt.Layouts.Services;
using Tomelt.Layouts.ViewModels;

namespace Tomelt.Layouts.Drivers {
    public class TextElementDriver : ElementDriver<Text> {
        private readonly IElementFilterProcessor _processor;

        public TextElementDriver(IElementFilterProcessor processor) {
            _processor = processor;
        }

        protected override EditorResult OnBuildEditor(Text element, ElementEditorContext context) {
            var viewModel = new TextEditorViewModel {
                Content = element.Content
            };
            var editor = context.ShapeFactory.EditorTemplate(TemplateName: "Elements.Text", Model: viewModel);

            if (context.Updater != null) {
                context.Updater.TryUpdateModel(viewModel, context.Prefix, null, null);
                element.Content = viewModel.Content;
            }
            
            return Editor(context, editor);
        }

        protected override void OnDisplaying(Text element, ElementDisplayingContext context) {
            context.ElementShape.ProcessedContent = _processor.ProcessContent(element.Content, "textarea", context.GetTokenData());
        }
    }
}