using System;
using System.Globalization;
using Tomelt.ContentManagement;
using Tomelt.ContentManagement.FieldStorage.InfosetStorage;

namespace Tomelt.MediaLibrary.Models {
    public class ImagePart : ContentPart {

        public int Width {
            get { return Convert.ToInt32(this.As<InfosetPart>().Get("ImagePart", "Width", "Value"), CultureInfo.InvariantCulture); }
            set { this.As<InfosetPart>().Set("ImagePart", "Width", "Value", Convert.ToString(value, CultureInfo.InvariantCulture)); }
        }

        public int Height {
            get { return Convert.ToInt32(this.As<InfosetPart>().Get("ImagePart", "Height", "Value"), CultureInfo.InvariantCulture); }
            set { this.As<InfosetPart>().Set("ImagePart", "Height", "Value", Convert.ToString(value, CultureInfo.InvariantCulture)); }
        }
   }
}