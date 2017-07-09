﻿using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tomelt.ContentManagement;
using Tomelt.DisplayManagement;
using Tomelt.DisplayManagement.Descriptors;
using Tomelt.Environment;
using Tomelt.UI.Navigation;

// ReSharper disable InconsistentNaming

namespace Tomelt.ContentPicker.Services {
    public class ContentPickerShapes : IShapeTableProvider {
        private readonly Work<INavigationManager> _navigationManager;
        private readonly Work<WorkContext> _workContext;
        private readonly Work<IShapeFactory> _shapeFactory;

        public ContentPickerShapes(
            Work<INavigationManager> navigationManager,
            Work<WorkContext> workContext,
            Work<IShapeFactory> shapeFactory) {
            _navigationManager = navigationManager;
            _workContext = workContext;
            _shapeFactory = shapeFactory;
        }

        public void Discover(ShapeTableBuilder builder) {
            builder.Describe("ContentPicker")
                .OnDisplaying(displaying => {
                    ContentItem contentItem = displaying.Shape.ContentItem;
                    if (contentItem != null) {
                        displaying.ShapeMetadata.Alternates.Add("ContentPicker_" + displaying.ShapeMetadata.DisplayType);
                    }
                });            
        }
        
        [Shape]
        public IHtmlString ContentPickerNavigation(dynamic Display) {

            IEnumerable<MenuItem> menuItems = _navigationManager.Value.BuildMenu("content-picker");

            var request = _workContext.Value.HttpContext.Request;

            // Set the currently selected path
            Stack<MenuItem> selectedPath = NavigationHelper.SetSelectedPath(menuItems, request, request.RequestContext.RouteData);

            dynamic shapeFactory = _shapeFactory.Value;

            // Populate local nav
            dynamic localMenuShape = shapeFactory.LocalMenu().MenuName("content-picker");
            NavigationHelper.PopulateLocalMenu(shapeFactory, localMenuShape, localMenuShape, selectedPath);
            return Display(localMenuShape);
        }
    }
}