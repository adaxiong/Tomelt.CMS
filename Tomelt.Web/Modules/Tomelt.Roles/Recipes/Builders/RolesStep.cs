using System.Linq;
using System.Xml.Linq;
using Tomelt.Data;
using Tomelt.Localization;
using Tomelt.Recipes.Services;
using Tomelt.Roles.Models;

namespace Tomelt.Roles.Recipes.Builders {
    public class RolesStep : RecipeBuilderStep {
        private readonly IRepository<RoleRecord> _roleRecordepository;

        public RolesStep(IRepository<RoleRecord> roleRecordRepository) {
            _roleRecordepository = roleRecordRepository;
        }

        public override string Name {
            get { return "Roles"; }
        }

        public override LocalizedString DisplayName {
            get { return T("Roles"); }
        }

        public override LocalizedString Description {
            get { return T("Exports user roles."); }
        }

        public override void Build(BuildContext context) {
            var roles = _roleRecordepository.Table.OrderBy(x => x.Name).ToList();

            if (!roles.Any())
                return;

            var root = new XElement("Roles");
            context.RecipeDocument.Element("Tomelt").Add(root);

            foreach (var role in roles) {
                root.Add(
                    new XElement("Role",
                        new XAttribute("Name", role.Name),
                        new XAttribute("Permissions", string.Join(",", role.RolesPermissions.Select(rolePermission => rolePermission.Permission.Name)))));
            }
        }
    }
}