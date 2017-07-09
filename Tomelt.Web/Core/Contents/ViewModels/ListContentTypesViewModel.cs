using System.Collections.Generic;
using Tomelt.ContentManagement.MetaData.Models;

namespace Tomelt.Core.Contents.ViewModels {
    public class ListContentTypesViewModel  {
        public IEnumerable<ContentTypeDefinition> Types { get; set; }
    }
}