using System.Web.Mvc;
using Tomelt.Localization;

namespace Tomelt.Mvc.Extensions {
    public static class ModelStateDictionaryExtensions {
        public static void AddModelError(this ModelStateDictionary modelStateDictionary, string key, LocalizedString errorMessage) {
            modelStateDictionary.AddModelError(key, errorMessage.ToString());
        }
    }
}
