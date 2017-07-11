using Tomelt.Environment.Extensions;
using Tomelt.UI.Navigation;

namespace Tomelt.Localization {
    [TomeltFeature("Tomelt.Localization.Transliteration")]
    public class AdminMenu : INavigationProvider {
        public Localizer T { get; set; }
        public string MenuName { get { return "admin"; } }

        public void GetNavigation(NavigationBuilder builder) {
            //builder
            //    .Add(T("Settings"), menu => menu
            //        .Add(T("Transliteration"), "10.0", subMenu => subMenu.Action("Index", "TransliterationAdmin", new { area = "Tomelt.Localization" })
            //            .Add(T("Settings"), "10.0", item => item.Action("Index", "TransliterationAdmin", new { area = "Tomelt.Localization" }).LocalNav())
            //        ));
        }
    }
}