using System.Collections.Generic;
using Tomelt.ContentManagement;
using Tomelt.ContentManagement.Handlers;
using Tomelt.ContentManagement.MetaData.Builders;
using Tomelt.Security;

namespace Tomelt.Roles.Models {
    public static class UserSimulation {
        public static IUser Create(string role) {
            var simulationType = new ContentTypeDefinitionBuilder().Named("User").Build();
            var simulation = new ContentItemBuilder(simulationType)
                .Weld<SimulatedUser>()
                .Weld<SimulatedUserRoles>()
                .Build();
            simulation.As<SimulatedUserRoles>().Roles = new[] {role};
            return simulation.As<IUser>();
        }

        class SimulatedUser : ContentPart, IUser {
            public string UserName { get { return null; } }
            public string Email { get { return null; } }
        }

        class SimulatedUserRoles : ContentPart, IUserRoles {
            public IList<string> Roles { get; set; }
        }
    }
}
