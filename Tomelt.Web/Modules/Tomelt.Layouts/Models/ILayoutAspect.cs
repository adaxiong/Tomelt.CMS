using Tomelt.ContentManagement;

namespace Tomelt.Layouts.Models {
    public interface ILayoutAspect : IContent {
        int? TemplateId { get; set; }
        string LayoutData { get; set; }
    }
}