using System;
using Tomelt.DisplayManagement;
using Tomelt.Environment.Extensions;
using Tomelt.Layouts.Framework.Display;
using Tomelt.Layouts.Services;
using Tomelt.Tokens;

namespace Tomelt.Layouts.Tokens {
    [TomeltFeature("Tomelt.Layouts.Tokens")]
    public class ElementTokens : Component, ITokenProvider {
        private readonly IElementManager _elementManager;
        private readonly IElementDisplay _elementDisplay;
        private readonly IShapeDisplay _shapeDisplay;

        public ElementTokens(IElementManager elementManager, IElementDisplay elementDisplay, IShapeDisplay shapeDisplay) {
            _elementManager = elementManager;
            _elementDisplay = elementDisplay;
            _shapeDisplay = shapeDisplay;
        }

        public void Describe(DescribeContext context) {
            context.For("Element", T("Element"), T("Element tokens."))
                .Token("Display:*", T("Display:<element type>"), T("Displays the specified element type."))
            ;
        }

        public void Evaluate(EvaluateContext context) {
            context.For("Element", "")
                .Token(token => token.StartsWith("Display:", StringComparison.OrdinalIgnoreCase) ? token.Substring("Display:".Length) : null, TokenValue);
        }

        private string TokenValue(string elementTypeName, string value) {
            var describeContext = DescribeElementsContext.Empty;
            var elementDescriptor = _elementManager.GetElementDescriptorByTypeName(describeContext, elementTypeName);

            if (elementDescriptor == null)
                return "";

            var element = _elementManager.ActivateElement(elementDescriptor);
            var elementShape = _elementDisplay.DisplayElement(element, null);
            var elementHtml = _shapeDisplay.Display(elementShape);

            return elementHtml;
        }
    }
}