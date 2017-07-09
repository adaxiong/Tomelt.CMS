using System.Collections.Generic;
using Tomelt.Layouts.Framework.Elements;

namespace Tomelt.Layouts.Services {
    public interface ILayoutSerializer : IDependency {
        IEnumerable<Element> Deserialize(string data, DescribeElementsContext describeContext);
        string Serialize(IEnumerable<Element> elements);
    }
}