using System.Collections.Generic;
using Tomelt.ContentManagement.MetaData;

namespace Tomelt.ContentTypes.ViewModels {
    public class EditFieldNameViewModel {
        /// <summary>
        /// The technical name of the field
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The display name of the field
        /// </summary>
        public string DisplayName { get; set; }
   }
}