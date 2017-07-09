using System.Collections.Generic;
using System.Web.Routing;
using Tomelt.Environment.Extensions.Models;
using Tomelt.Events;

namespace Tomelt.Packaging.Events {
    public interface IExtensionDisplayEventHandler : IEventHandler {
        /// <summary>
        /// Called before an extension is displayed
        /// </summary>
        IEnumerable<string> Displaying(ExtensionDescriptor extensionDescriptor, RequestContext requestContext);
    }
}