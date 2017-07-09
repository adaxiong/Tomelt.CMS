using Tomelt.ContentManagement;
using Tomelt.ContentManagement.FieldStorage.InfosetStorage;

namespace Tomelt.MediaLibrary.Models {
    public class WebSearchSettingsPart : ContentPart {

        public string ApiKey {
            get { return this.As<InfosetPart>().Get<WebSearchSettingsPart>("ApiKey"); }
            set { this.As<InfosetPart>().Set<WebSearchSettingsPart>("ApiKey", value); }
        }
   }
}