using Tomelt.Layouts.Elements;
using Tomelt.Layouts.Framework.Display;
using Tomelt.Layouts.Framework.Drivers;
using Tomelt.Layouts.Helpers;
using Tomelt.Layouts.Services;
using Tomelt.Layouts.ViewModels;

namespace Tomelt.Layouts.Drivers {
    public class HtmlElementDriver : ElementDriver<Html> {
        private readonly IElementFilterProcessor _processor;

        public HtmlElementDriver(IElementFilterProcessor processor) {
            _processor = processor;
        }

        protected override EditorResult OnBuildEditor(Html element, ElementEditorContext context) {
            var viewModel = new HtmlEditorViewModel {
                Text = element.Content
            };
            var editor = context.ShapeFactory.EditorTemplate(TemplateName: "Elements.Html", Model: viewModel);

            if (context.Updater != null) {
                context.Updater.TryUpdateModel(viewModel, context.Prefix, null, null);
                element.Content = viewModel.Text;
            }
            
            return Editor(context, editor);
        }

        protected override void OnDisplaying(Html element, ElementDisplayingContext context) {
            context.ElementShape.ProcessedContent = _processor.ProcessContent(element.Content, "html", context.GetTokenData());
        }
    }
}