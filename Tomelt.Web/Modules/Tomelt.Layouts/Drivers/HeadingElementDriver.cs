using Tomelt.Layouts.Elements;
using Tomelt.Layouts.Framework.Display;
using Tomelt.Layouts.Framework.Drivers;
using Tomelt.Layouts.Helpers;
using Tomelt.Layouts.Services;
using Tomelt.Layouts.ViewModels;

namespace Tomelt.Layouts.Drivers {
    public class HeadingElementDriver : ElementDriver<Heading> {
        private readonly IElementFilterProcessor _processor;

        public HeadingElementDriver(IElementFilterProcessor processor) {
            _processor = processor;
        }

        protected override EditorResult OnBuildEditor(Heading element, ElementEditorContext context) {
            var viewModel = new HeadingEditorViewModel {
                Text = element.Content,
                Level = element.Level
            };
            var editor = context.ShapeFactory.EditorTemplate(TemplateName: "Elements.Heading", Model: viewModel);

            if (context.Updater != null) {
                context.Updater.TryUpdateModel(viewModel, context.Prefix, null, null);
                element.Content = viewModel.Text;
                element.Level = viewModel.Level;
            }
            
            return Editor(context, editor);
        }

        protected override void OnDisplaying(Heading element, ElementDisplayingContext context) {
            context.ElementShape.ProcessedContent = _processor.ProcessContent(element.Content, "html", context.GetTokenData());
            context.ElementShape.Level = element.Level;
        }
    }
}