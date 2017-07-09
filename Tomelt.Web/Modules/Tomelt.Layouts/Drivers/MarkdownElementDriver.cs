using Tomelt.Environment.Extensions;
using Tomelt.Layouts.Framework.Display;
using Tomelt.Layouts.Framework.Drivers;
using Tomelt.Layouts.Helpers;
using Tomelt.Layouts.Services;
using Tomelt.Layouts.ViewModels;
using MarkdownElement = Tomelt.Layouts.Elements.Markdown;

namespace Tomelt.Layouts.Drivers {
    [TomeltFeature("Tomelt.Layouts.Markdown")]
    public class MarkdownElementDriver : ElementDriver<MarkdownElement> {
        private readonly IElementFilterProcessor _processor;
        public MarkdownElementDriver(IElementFilterProcessor processor) {
            _processor = processor;
        }

        protected override EditorResult OnBuildEditor(MarkdownElement element, ElementEditorContext context) {
            var viewModel = new MarkdownEditorViewModel {
                Text = element.Content
            };
            var editor = context.ShapeFactory.EditorTemplate(TemplateName: "Elements.Markdown", Model: viewModel);

            if (context.Updater != null) {
                context.Updater.TryUpdateModel(viewModel, context.Prefix, null, null);
                element.Content = viewModel.Text;
            }
            
            return Editor(context, editor);
        }

        protected override void OnDisplaying(MarkdownElement element, ElementDisplayingContext context) {
            context.ElementShape.ProcessedContent = _processor.ProcessContent(element.Content, "markdown", context.GetTokenData());
        }
    }
}