using System;
using System.Collections.Generic;
using Tomelt.DisplayManagement;
using Tomelt.Forms.Services;
using Tomelt.Layouts.Elements;
using Tomelt.Layouts.Framework.Display;
using Tomelt.Layouts.Framework.Drivers;
using Tomelt.Layouts.Services;

namespace Tomelt.Layouts.Drivers {
    public class ShapeElementDriver : FormsElementDriver<Shape> {
        private readonly IShapeFactory _shapeFactory;

        public ShapeElementDriver(IFormsBasedElementServices formsServices, IShapeFactory shapeFactory)
            : base(formsServices) {
            _shapeFactory = shapeFactory;
        }

        protected override IEnumerable<string> FormNames {
            get { yield return "ShapeElement"; }
        }

        protected override void OnDisplaying(Shape element, ElementDisplayingContext context) {
            if (String.IsNullOrWhiteSpace(element.ShapeType))
                return;

            var shape = _shapeFactory.Create(element.ShapeType);
            context.ElementShape.Shape = shape;
        }

        protected override void DescribeForm(DescribeContext context) {
            context.Form("ShapeElement", shapeFactory => {
                var factory = (dynamic)shapeFactory;
                var form = factory.Fieldset(
                    Id: "ShapeElement",
                    _ShapeType: factory.Textbox(
                        Id: "shapeType",
                        Name: "ShapeType",
                        Title: T("Shape Type"),
                        Description: T("The shape type name to dislay."),
                        Classes: new[] { "text", "large", "tokenized" }));

                return form;
            });
        }
    }
}