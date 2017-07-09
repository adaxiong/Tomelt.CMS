using System;
using Tomelt.ContentManagement.FieldStorage;
using Tomelt.ContentManagement.MetaData.Models;

namespace Tomelt.ContentManagement.MetaData {
    public class ContentFieldInfo {
        public string FieldTypeName { get; set; }
        public Func<ContentPartFieldDefinition, IFieldStorage, ContentField> Factory { get; set; }
    }
}
