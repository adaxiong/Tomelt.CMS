using System;
using Tomelt.ContentManagement.MetaData.Models;

namespace Tomelt.ContentManagement.MetaData {
    public class ContentPartInfo {
        public string PartName { get; set; }
        public Func<ContentTypePartDefinition, ContentPart> Factory { get; set; }
    }
}
