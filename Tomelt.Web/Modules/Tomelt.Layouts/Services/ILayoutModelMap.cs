using Newtonsoft.Json.Linq;
using Tomelt.Layouts.Framework.Elements;

namespace Tomelt.Layouts.Services {
    public interface ILayoutModelMap : IDependency {
        int Priority { get; }
        string LayoutElementType { get; }
        bool CanMap(Element element);
        Element ToElement(IElementManager elementManager, DescribeElementsContext describeContext, JToken node);
        void FromElement(Element element, DescribeElementsContext describeContext, JToken node);
    }
}