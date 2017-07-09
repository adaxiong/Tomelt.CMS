using Tomelt.ContentManagement;
using Tomelt.Layouts.Models;
using Tomelt.Layouts.ViewModels;

namespace Tomelt.Layouts.Services {
    public interface ILayoutEditorFactory : IDependency {
        LayoutEditor Create(LayoutPart layoutPart);
        LayoutEditor Create(string layoutData, string sessionKey, int? templateId = null, IContent content = null);
    }
}