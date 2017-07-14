using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tomelt.ContentManagement;
using Tomelt.ContentManagement.FieldStorage;

namespace Tomelt.EasyUIFields.Fields
{
    public class TextBoxField: ContentField
    {
        public string Value
        {
            get { return Storage.Get<string>(); }
            set { Storage.Set(value ?? String.Empty); }
        }
    }
}