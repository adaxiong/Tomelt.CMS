using System.Linq;
using Tomelt.Data;
using Tomelt.ContentManagement.Handlers;
using Tomelt.Roles.Models;

namespace Tomelt.Roles.Handlers {
    public class UserRolesPartHandler : ContentHandler {
        private readonly IRepository<UserRolesPartRecord> _userRolesRepository;

        public UserRolesPartHandler(IRepository<UserRolesPartRecord> userRolesRepository) {
            _userRolesRepository = userRolesRepository;

            Filters.Add(new ActivatingFilter<UserRolesPart>("User"));
            OnInitialized<UserRolesPart>((context, userRoles) => userRoles._roles.Loader(() => _userRolesRepository
                .Fetch(x => x.UserId == context.ContentItem.Id)
                .Select(x => x.Role.Name).ToList()));
        }
    }
}