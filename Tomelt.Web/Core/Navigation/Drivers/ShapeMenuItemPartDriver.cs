using System;
using Tomelt.ContentManagement;
using Tomelt.ContentManagement.Drivers;
using Tomelt.ContentManagement.Handlers;
using Tomelt.Core.Navigation.Models;
using Tomelt.Localization;

namespace Tomelt.Core.Navigation.Drivers {
    public class ShapeMenuItemPartDriver : ContentPartDriver<ShapeMenuItemPart> {
        private const string TemplateName = "Parts.ShapeMenuItemPart.Edit";

        public ShapeMenuItemPartDriver(ITomeltServices services) {
            T = NullLocalizer.Instance;
            Services = services;
        }

        public Localizer T { get; set; }
        public ITomeltServices Services { get; set; }

        protected override string Prefix { get { return "ShapeMenuItemPart"; } }

        protected override DriverResult Editor(ShapeMenuItemPart part, dynamic shapeHelper) {
            return ContentShape("Parts_ShapeMenuItemPart_Edit", () => {

                return shapeHelper.EditorTemplate(TemplateName: TemplateName, Model: part, Prefix: Prefix);
            });
        }

        protected override DriverResult Editor(ShapeMenuItemPart part, IUpdateModel updater, dynamic shapeHelper) {
            if (updater.TryUpdateModel(part, Prefix, null, null)) {
                if (String.IsNullOrWhiteSpace(part.ShapeType)) {
                    updater.AddModelError("ShapeType", T("The Shape Type is mandatory."));
                }
            }

            return Editor(part, shapeHelper);
        }

        protected override void Importing(ShapeMenuItemPart part, ImportContentContext context) {
            // Don't do anything if the tag is not specified.
            if (context.Data.Element(part.PartDefinition.Name) == null) {
                return;
            }

            context.ImportAttribute(part.PartDefinition.Name, "ShapeType", x => part.ShapeType = x);
        }

        protected override void Exporting(ShapeMenuItemPart part, ExportContentContext context) {
            context.Element(part.PartDefinition.Name).SetAttributeValue("ShapeType", part.ShapeType);
        }
    }
}
