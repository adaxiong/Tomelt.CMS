using System.Collections.Generic;
using Tomelt.ContentManagement;

namespace Tomelt.Roles.Models {
    public interface IUserRoles : IContent {
        IList<string> Roles { get; }
    }
}