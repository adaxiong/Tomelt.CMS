using System.Collections.Generic;
using Tomelt.Autoroute.Models;
using Tomelt.Autoroute.Settings;

namespace Tomelt.Autoroute.Services {

    /// <summary>
    /// Provides main services for Autoroute module
    /// </summary>
    public interface IAutorouteService : IDependency {

        string GenerateAlias(AutoroutePart part);
        void PublishAlias(AutoroutePart part);
        void RemoveAliases(AutoroutePart part);
        void CreatePattern(string contentType, string name, string pattern, string description, bool makeDefault);
        RoutePattern GetDefaultPattern(string contentType, string culture);
        IEnumerable<RoutePattern> GetPatterns(string contentType);
        bool ProcessPath(AutoroutePart part);
        bool IsPathValid(string slug);
    }
}
