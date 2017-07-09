using System.Linq;
using Tomelt.ContentManagement;
using Tomelt.Projections.Models;

namespace Tomelt.Projections.Services {
    public interface IQueryService : IDependency {
        QueryPart GetQuery(int id);

        QueryPart CreateQuery(string name);
        void DeleteQuery(int id);
    }
}