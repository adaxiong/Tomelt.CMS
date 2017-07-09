using Newtonsoft.Json.Linq;
using Tomelt.Layouts.Elements;
using Tomelt.Layouts.Framework.Elements;

namespace Tomelt.Layouts.Services {
    public interface IElementSerializer : IDependency {
        Element Deserialize(string data, DescribeElementsContext describeContext);
        string Serialize(Element element);
        object ToDto(Element element, int index = 0);
        Element ParseNode(JToken node, Container parent, int index, DescribeElementsContext describeContext);
    }
}