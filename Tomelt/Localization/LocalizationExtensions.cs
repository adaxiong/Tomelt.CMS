using System.Globalization;
using Tomelt.ContentManagement;
using Tomelt.ContentManagement.Aspects;

namespace Tomelt.Localization {
    public static class LocalizationExtensions {
        public static CultureInfo CurrentCultureInfo(this WorkContext workContext) {
            return CultureInfo.GetCultureInfo(workContext.CurrentCulture);
        }

        public static string GetTextDirection(this WorkContext workContext) {
            return workContext.GetTextDirection(null);
        }

        public static string GetTextDirection(this WorkContext workContext, IContent content) {
            var culture = workContext.CurrentSite.SiteCulture;
            if (content != null && content.Has<ILocalizableAspect>()) {
                culture = content.As<ILocalizableAspect>().Culture ?? culture;
            }

            return CultureInfo.GetCultureInfo(culture).TextInfo.IsRightToLeft ? "rtl" : "ltr"; ;
        }
    }
}