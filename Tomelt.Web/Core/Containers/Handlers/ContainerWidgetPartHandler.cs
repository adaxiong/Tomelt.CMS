using Tomelt.ContentManagement.Handlers;
using Tomelt.Core.Containers.Models;
using Tomelt.Data;

namespace Tomelt.Core.Containers.Handlers {
    public class ContainerWidgetPartHandler : ContentHandler {
        public ContainerWidgetPartHandler(IRepository<ContainerWidgetPartRecord> repository) {
            Filters.Add(StorageFilter.For(repository));
            OnInitializing<ContainerWidgetPart>((context, part) => {
                part.Record.ContainerId = 0;
                part.Record.PageSize = 5;
            });
        }
    }
}