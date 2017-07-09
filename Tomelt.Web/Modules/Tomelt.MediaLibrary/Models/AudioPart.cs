using System;
using System.Globalization;
using Tomelt.ContentManagement;
using Tomelt.ContentManagement.FieldStorage.InfosetStorage;

namespace Tomelt.MediaLibrary.Models {
    public class AudioPart : ContentPart {
        public int Length {
            get { return Convert.ToInt32(this.As<InfosetPart>().Get("AudioPart", "Length", "Value"), CultureInfo.InvariantCulture); }
            set { this.As<InfosetPart>().Set("AudioPart", "Length", "Value", Convert.ToString(value, CultureInfo.InvariantCulture)); }
        }
    }
}