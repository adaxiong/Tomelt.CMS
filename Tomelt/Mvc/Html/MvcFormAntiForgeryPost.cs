using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Tomelt.Mvc.Html {
    public class MvcFormAntiForgeryPost : MvcForm {
        private readonly HtmlHelper _htmlHelper;

        public MvcFormAntiForgeryPost(HtmlHelper htmlHelper) : base(htmlHelper.ViewContext) {
            _htmlHelper = htmlHelper;
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                _htmlHelper.ViewContext.Writer.Write(_htmlHelper.AntiForgeryTokenTomelt());
            }

            base.Dispose(disposing);
        }
    }
}