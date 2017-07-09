using System;
using Tomelt.DisplayManagement;
using Tomelt.Forms.Services;
using Tomelt.Localization;

namespace Tomelt.Workflows.Forms {
    public class BranchForms : IFormProvider {
        protected dynamic Shape { get; set; }
        public Localizer T { get; set; }

        public BranchForms(IShapeFactory shapeFactory) {
            Shape = shapeFactory;
            T = NullLocalizer.Instance;
        }

        public void Describe(DescribeContext context) {
            Func<IShapeFactory, dynamic> form =
                shape => {

                    var f = Shape.Form(
                        Id: "BranchNames",
                        _Message: Shape.Textbox(
                            Id: "branches", Name: "Branches",
                            Title: T("Available branches."),
                            Description: T("A comma separated list of names."),
                            Classes: new[] { "text medium" })
                        );

                    return f;
                };

            context.Form("ActivityBranch", form);

        }
    }
}