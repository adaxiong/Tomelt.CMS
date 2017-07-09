using Tomelt.Caching;
using Tomelt.ContentManagement;
using Tomelt.Environment.Extensions;
using Tomelt.Environment.Extensions.Models;
using Tomelt.Themes.Models;

namespace Tomelt.Themes.Services {
    public interface ISiteThemeService : IDependency {
        ExtensionDescriptor GetSiteTheme();
        void SetSiteTheme(string themeName);
        string GetCurrentThemeName();
    }

    public class SiteThemeService : ISiteThemeService {
        public const string CurrentThemeSignal = "SiteCurrentTheme";

        private readonly IExtensionManager _extensionManager;
        private readonly ICacheManager _cacheManager;
        private readonly ISignals _signals;
        private readonly ITomeltServices _tomeltServices;

        public SiteThemeService(
            ITomeltServices tomeltServices,
            IExtensionManager extensionManager,
            ICacheManager cacheManager,
            ISignals signals) {

            _tomeltServices = tomeltServices;
            _extensionManager = extensionManager;
            _cacheManager = cacheManager;
            _signals = signals;
        }

        public ExtensionDescriptor GetSiteTheme() {
            string currentThemeName = GetCurrentThemeName();
            return string.IsNullOrEmpty(currentThemeName) ? null : _extensionManager.GetExtension(GetCurrentThemeName());
        }

        public void SetSiteTheme(string themeName) {
            var site = _tomeltServices.WorkContext.CurrentSite;
            site.As<ThemeSiteSettingsPart>().CurrentThemeName = themeName;

            _signals.Trigger(CurrentThemeSignal);
        }

        public string GetCurrentThemeName() {
            return _cacheManager.Get("CurrentThemeName", ctx => {
                ctx.Monitor(_signals.When(CurrentThemeSignal));
                return _tomeltServices.WorkContext.CurrentSite.As<ThemeSiteSettingsPart>().CurrentThemeName;
            });
        }
    }
}
