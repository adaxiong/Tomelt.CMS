using Tomelt.DisplayManagement;
using Tomelt.Forms.Services;
using Tomelt.Localization;

namespace Tomelt.Workflows.Forms {
    public class RedirectActionForm : IFormProvider {
        protected dynamic New { get; set; }
        public Localizer T { get; set; }

        public RedirectActionForm(IShapeFactory shapeFactory) {
            New = shapeFactory;
            T = NullLocalizer.Instance;
        }

        public void Describe(DescribeContext context) {
            context.Form("ActionRedirect",
                shape => New.Form(
                Id: "ActionRedirect",
                _Url: New.Textbox(
                    Id: "Url", Name: "Url",
                    Title: T("Url"),
                    Description: T("The url to redirect to."),
                    Classes: new[] { "text large", "tokenized" })
                )
            );
        }
    }
}