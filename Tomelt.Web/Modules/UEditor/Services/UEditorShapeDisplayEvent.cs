using System;
using System.Globalization;
using System.Linq;
using Tomelt;
using Tomelt.Caching;
using Tomelt.DisplayManagement.Implementation;
using Tomelt.FileSystems.VirtualPath;

namespace UEditor.Services {
    public class UEditorShapeDisplayEvent : ShapeDisplayEvents {

        public override void Displaying(ShapeDisplayingContext context) {
            if (String.CompareOrdinal(context.ShapeMetadata.Type, "Body_Editor") != 0) {
                return;
            }
            if (!String.Equals(context.Shape.EditorFlavor, "ueditor", StringComparison.InvariantCultureIgnoreCase)) {
            }
        }

       
    }
}