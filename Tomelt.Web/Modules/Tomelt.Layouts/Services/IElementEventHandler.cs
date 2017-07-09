using Tomelt.Events;
using Tomelt.Layouts.Framework.Display;
using Tomelt.Layouts.Framework.Drivers;
using Tomelt.Layouts.Framework.Elements;

namespace Tomelt.Layouts.Services {
    public interface IElementEventHandler : IEventHandler {
        void Creating(ElementCreatingContext context);
        void Created(ElementCreatedContext context);
        void CreatingDisplay(ElementCreatingDisplayShapeContext context);
        void Displaying(ElementDisplayingContext context);
        void Displayed(ElementDisplayedContext context);
        void BuildEditor(ElementEditorContext context);
        void UpdateEditor(ElementEditorContext context);
        void LayoutSaving(ElementSavingContext context);
        void Removing(ElementRemovingContext context);
        void Exporting(ExportElementContext context);
        void Exported(ExportElementContext context);
        void Importing(ImportElementContext context);
        void Imported(ImportElementContext context);
        void ImportCompleted(ImportElementContext context);
    }
}