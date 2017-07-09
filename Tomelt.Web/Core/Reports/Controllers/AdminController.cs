using System.Linq;
using System.Web.Mvc;
using Tomelt.Core.Reports.ViewModels;
using Tomelt.Localization;
using Tomelt.Reports.Services;
using Tomelt.Security;

namespace Tomelt.Core.Reports.Controllers {
    public class AdminController : Controller {
        private readonly IReportsManager _reportsManager;

        public AdminController(
            ITomeltServices services, 
            IReportsManager reportsManager) {
            Services = services;
            _reportsManager = reportsManager;
            T = NullLocalizer.Instance;
        }

        public ITomeltServices Services { get; set; }
        public Localizer T { get; set; }

        public ActionResult Index() {
            if (!Services.Authorizer.Authorize(StandardPermissions.SiteOwner, T("Not authorized to list reports")))
                return new HttpUnauthorizedResult();

            var model = new ReportsAdminIndexViewModel { Reports = _reportsManager.GetReports().ToList() };

            return View(model);
        }

        public ActionResult Display(int id) {
            if (!Services.Authorizer.Authorize(StandardPermissions.SiteOwner, T("Not authorized to display report")))
                return new HttpUnauthorizedResult();

            var model = new DisplayReportViewModel { Report = _reportsManager.Get(id) };

            return View(model);
        }

    }
}