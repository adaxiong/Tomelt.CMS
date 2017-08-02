using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tomelt.ContentManagement;
using Tomelt.ContentManagement.FieldStorage;

namespace UEditor.Fields
{
    public class FilePickField: ContentField
    {
        public string Path
        {
            get { return Storage.Get<string>(); }
            set { Storage.Set(value ?? String.Empty); }
        }
    }
}