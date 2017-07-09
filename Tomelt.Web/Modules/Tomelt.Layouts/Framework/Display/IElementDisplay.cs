using System.Collections.Generic;
using Tomelt.ContentManagement;
using Tomelt.Layouts.Framework.Elements;

namespace Tomelt.Layouts.Framework.Display {
    public interface IElementDisplay : IDependency {
        dynamic DisplayElement(Element element, IContent content, string displayType = null, IUpdateModel updater = null);
        dynamic DisplayElements(IEnumerable<Element> elements, IContent content, string displayType = null, IUpdateModel updater = null);
    }
}