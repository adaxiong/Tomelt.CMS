using Tomelt.ContentManagement;
using Tomelt.ContentManagement.Aspects;
using Tomelt.Templates.Settings;

namespace Tomelt.Templates.Models {
    public class ShapePart : ContentPart {
        public string Name {
            get { return this.As<ITitleAspect>().Title; }
        }

        public string ProcessorName {
            get { return TypePartDefinition.Settings.GetModel<ShapePartSettings>().Processor; }
        }

        public string Template {
            get { return this.Retrieve(x => x.Template); }
            set { this.Store(x => x.Template, value); }
        }

        public RenderingMode RenderingMode {
            get { return this.Retrieve(x => x.RenderingMode); }
            set { this.Store(x => x.RenderingMode, value); }
        }
    }

    public enum RenderingMode {
        FrontEndAndAdmin,
        FrontEnd,
        Admin
    }
}