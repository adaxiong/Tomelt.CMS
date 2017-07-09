using System.Collections.Generic;
using Tomelt.Data.Conventions;

namespace Tomelt.Roles.Models {
    public class RoleRecord {
        public RoleRecord() {
            RolesPermissions = new List<RolesPermissionsRecord>();
        }

        public virtual int Id { get; set; }
        public virtual string Name { get; set; }

        [CascadeAllDeleteOrphan]
        public virtual IList<RolesPermissionsRecord> RolesPermissions { get; set; }
    }
}