using System.Linq;
using Tomelt.Autoroute.Models;
using Tomelt.ContentManagement;
using Tomelt.Data;

namespace Tomelt.Autoroute.Services {
    public class PathResolutionService : IPathResolutionService {
        private readonly IContentManager _contentManager;
        private readonly IRepository<AutoroutePartRecord> _autorouteRepository;

        public PathResolutionService(
            IRepository<AutoroutePartRecord> autorouteRepository,
            IContentManager contentManager) {
            _contentManager = contentManager;
            _autorouteRepository = autorouteRepository;
        }

        public AutoroutePart GetPath(string path) {
            var autorouteRecord = _autorouteRepository.Table
                .FirstOrDefault(part => part.DisplayAlias == path && part.ContentItemVersionRecord.Published);

            if (autorouteRecord == null) {
                return null;
            }

            return _contentManager.Get(autorouteRecord.ContentItemRecord.Id).As<AutoroutePart>();
        }
    }
}
