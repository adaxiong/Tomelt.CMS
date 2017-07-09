using Tomelt.Layouts.Framework.Display;
using Tomelt.Layouts.Framework.Elements;

namespace Tomelt.Layouts.Framework.Drivers {
    public interface IElementDriver : IDependency {
        int Priority { get; }
        EditorResult BuildEditor(ElementEditorContext context);
        EditorResult UpdateEditor(ElementEditorContext context);
        void CreatingDisplay(ElementCreatingDisplayShapeContext context);
        void Displaying(ElementDisplayingContext context);
        void Displayed(ElementDisplayedContext context);
        void LayoutSaving(ElementSavingContext context);
        void Removing(ElementRemovingContext context);
        void Exporting(ExportElementContext context);
        void Exported(ExportElementContext context);
        void Importing(ImportElementContext context);
        void Imported(ImportElementContext context);
        void ImportCompleted(ImportElementContext context);
    }
}