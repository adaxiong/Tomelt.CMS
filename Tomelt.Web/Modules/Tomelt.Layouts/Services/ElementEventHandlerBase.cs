using Tomelt.Layouts.Framework.Display;
using Tomelt.Layouts.Framework.Drivers;
using Tomelt.Layouts.Framework.Elements;

namespace Tomelt.Layouts.Services {
    public abstract class ElementEventHandlerBase : Component, IElementEventHandler {
        public virtual void Creating(ElementCreatingContext context) { }
        public virtual void Created(ElementCreatedContext context) { }
        public virtual void CreatingDisplay(ElementCreatingDisplayShapeContext context) { }
        public virtual void Displaying(ElementDisplayingContext context) { }
        public virtual void Displayed(ElementDisplayedContext context) { }
        public virtual void BuildEditor(ElementEditorContext context) { }
        public virtual void UpdateEditor(ElementEditorContext context) { }
        public virtual void LayoutSaving(ElementSavingContext context) { }
        public virtual void Removing(ElementRemovingContext context) { }
        public virtual void Exporting(ExportElementContext context) { }
        public virtual void Exported(ExportElementContext context) { }
        public virtual void Importing(ImportElementContext context) { }
        public virtual void Imported(ImportElementContext context) { }
        public virtual void ImportCompleted(ImportElementContext context) { }
    }
}