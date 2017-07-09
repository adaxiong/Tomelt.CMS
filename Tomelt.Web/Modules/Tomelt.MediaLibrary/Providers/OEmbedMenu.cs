using Tomelt.Localization;
using Tomelt.UI.Navigation;

namespace Tomelt.MediaLibrary.Providers {
    public class OEmbedMenu : INavigationProvider {
        public Localizer T { get; set; }

        public OEmbedMenu() {
            T = NullLocalizer.Instance;
        }

        public string MenuName { get { return "mediaproviders"; } }

        public void GetNavigation(NavigationBuilder builder) {
            builder.AddImageSet("oembed")
                .Add(T("Media Url"), "10", 
                    menu => menu.Action("Index", "OEmbed", new { area = "Tomelt.MediaLibrary" })
                        .Permission(Permissions.ManageOwnMedia));
        }
    }
}