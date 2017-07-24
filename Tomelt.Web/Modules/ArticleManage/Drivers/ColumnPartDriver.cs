using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ArticleManage.Models;
using Tomelt;
using Tomelt.ContentManagement.Drivers;

namespace ArticleManage.Drivers
{
    public class ColumnPartDriver: ContentPartDriver<ColumnPart>
    {
        public ITomeltServices TomeltServices { get; set; }
        private const string TemplateName = "Parts/Column";

        public ColumnPartDriver(ITomeltServices tomeltServices)
        {
            TomeltServices = tomeltServices;
        }
        protected override DriverResult Display(ColumnPart part, string displayType, dynamic shapeHelper)
        {
            return ContentShape("Parts_Column", () => shapeHelper.Parts_Column);
        }
        protected override DriverResult Editor(ColumnPart part, dynamic shapeHelper)
        {
            return ContentShape("Parts_Column_Edit", () => shapeHelper.EditorTemplate(TemplateName: TemplateName, Model: part, Prefix: Prefix));
        }
    }
}