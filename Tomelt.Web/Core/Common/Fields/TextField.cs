using Tomelt.ContentManagement;
using Tomelt.ContentManagement.FieldStorage;

namespace Tomelt.Core.Common.Fields {
    public class TextField : ContentField {
        public string Value {
            get { return Storage.Get<string>(); }
            set { Storage.Set(value); }
        }
    }
}
