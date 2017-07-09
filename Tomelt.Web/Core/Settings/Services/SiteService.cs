using System.Linq;
using Tomelt.Caching;
using Tomelt.Core.Settings.Models;
using Tomelt.ContentManagement;
using Tomelt.Settings;

namespace Tomelt.Core.Settings.Services {
    public class SiteService : ISiteService {
        private readonly IContentManager _contentManager;
        private readonly ICacheManager _cacheManager;

        public SiteService(
            IContentManager contentManager,
            ICacheManager cacheManager) {
            _contentManager = contentManager;
            _cacheManager = cacheManager;
        }

        public ISite GetSiteSettings() {
            var siteId = _cacheManager.Get("SiteId", true, ctx => {
                var site = _contentManager.Query("Site")
                    .List()
                    .FirstOrDefault();

                if (site == null) {
                    site = _contentManager.Create<SiteSettingsPart>("Site").ContentItem;
                }

                return site.Id;
            });

            return _contentManager.Get<ISite>(siteId, VersionOptions.Published);
        }
    }
}