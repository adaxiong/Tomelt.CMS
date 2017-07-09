using System;
using System.Globalization;
using Tomelt.ContentManagement;
using Tomelt.ContentManagement.FieldStorage.InfosetStorage;

namespace Tomelt.MediaLibrary.Models {
    public class DocumentPart : ContentPart {
        public long Length {
            get { return Convert.ToInt64(this.As<InfosetPart>().Get("DocumentPart", "Length", "Value"), CultureInfo.InvariantCulture); }
            set { this.As<InfosetPart>().Set("DocumentPart", "Length", "Value", Convert.ToString(value, CultureInfo.InvariantCulture)); }
        }
    }
}