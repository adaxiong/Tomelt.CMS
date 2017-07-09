using Tomelt.Localization;
using Tomelt.UI.Navigation;

namespace Tomelt.MediaLibrary.Providers {
    public class ClientStorageMenu : INavigationProvider {
        public Localizer T { get; set; }

        public ClientStorageMenu() {
            T = NullLocalizer.Instance;
        }

        public string MenuName { get { return "mediaproviders"; } }

        public void GetNavigation(NavigationBuilder builder) {
            builder.AddImageSet("clientstorage")
                .Add(T("My Computer"), "5", 
                    menu => menu.Action("Index", "ClientStorage", new { area = "Tomelt.MediaLibrary" })
                        .Permission(Permissions.ManageOwnMedia));
        }
    }
}