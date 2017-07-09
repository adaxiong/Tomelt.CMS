using System.Web.Mvc;
using Tomelt.Environment.Extensions;
using Tomelt.Localization.Providers;
using Tomelt.Mvc.Extensions;
using Tomelt.UI.Admin;

namespace Tomelt.Localization.Controllers {
    [TomeltFeature("Tomelt.Localization.CultureSelector")]
    [Admin]
    public class AdminCultureSelectorController : Controller {
        private readonly ICultureStorageProvider _cultureStorageProvider;

        public AdminCultureSelectorController(ICultureStorageProvider cultureStorageProvider) {
            _cultureStorageProvider = cultureStorageProvider;
        }

        public ActionResult ChangeCulture(string culture, string returnUrl) {
            _cultureStorageProvider.SetCulture(culture);

            return this.RedirectLocal(returnUrl);
        }
    }
}