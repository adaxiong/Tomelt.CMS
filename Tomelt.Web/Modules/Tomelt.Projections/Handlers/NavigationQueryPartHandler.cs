using Tomelt.Data;
using Tomelt.ContentManagement.Handlers;
using Tomelt.Projections.Models;

namespace Tomelt.Projections.Handlers {
    public class NavigationQueryPartHandler : ContentHandler {
        public NavigationQueryPartHandler(IRepository<NavigationQueryPartRecord> navigationQueryRepository) {
            Filters.Add(StorageFilter.For(navigationQueryRepository));
        }
    }
}