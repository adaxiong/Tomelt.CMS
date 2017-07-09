using Tomelt.DisplayManagement;

namespace Tomelt.ContentManagement.Handlers {
    public class BuildEditorContext : BuildShapeContext {
        public BuildEditorContext(IShape model, IContent content, string groupId, IShapeFactory shapeFactory)
            : base(model, content, groupId, shapeFactory) {
        }
    }
}