using System.Web.Mvc;
using Tomelt.Themes;

namespace Tomelt.Core.Common.Controllers {
    [Themed]
    public class ErrorController : Controller {

        public ActionResult NotFound(string url) {
            return HttpNotFound();
        }
    }
}