using System.Xml.Linq;

namespace Tomelt.Core.XmlRpc {
    public interface IXmlRpcHandler : IDependency {
        void SetCapabilities(XElement element);
        void Process(XmlRpcContext context);
    }
}