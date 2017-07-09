using Tomelt.Data;
using Tomelt.ContentManagement.Handlers;
using Tomelt.Projections.Models;

namespace Tomelt.Projections.Handlers {
    public class ProjectionPartHandler : ContentHandler {
        public ProjectionPartHandler(IRepository<ProjectionPartRecord> projecRepository) {
            Filters.Add(StorageFilter.For(projecRepository));
        }
    }
}