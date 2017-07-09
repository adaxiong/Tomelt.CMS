using System;
using Tomelt.DisplayManagement;
using Tomelt.Environment.Extensions;
using Tomelt.Events;
using Tomelt.Localization;

namespace Tomelt.Scripting.Rules {
    public interface IFormProvider : IEventHandler {
        void Describe(dynamic context);
    }

    [TomeltFeature("Tomelt.Scripting.Rules")]
    public class ConditionForms : IFormProvider {
        protected dynamic Shape { get; set; }
        public Localizer T { get; set; }

        public ConditionForms(IShapeFactory shapeFactory) {
            Shape = shapeFactory;
            T = NullLocalizer.Instance;
        }

        public void Describe(dynamic context) {
            Func<IShapeFactory, dynamic> form =
                shape => Shape.Form(
                Id: "ScriptCondition",
                _Description: Shape.Textbox(
                    Id: "description", Name: "description",
                    Title: T("Description"),
                    Description: T("Message that will be displayed in the Actions list."),
                    Classes: new[] { "text medium" }),
                _Condition: Shape.TextArea(
                    Id: "condition", Name: "condition",
                    Title: T("Condition"),
                    Description: T("Enter a valid boolean expression to evaluate."),
                    Classes: new[] { "tokenized" })
                );

            context.Form("ScriptCondition", form);
        }
    }
}