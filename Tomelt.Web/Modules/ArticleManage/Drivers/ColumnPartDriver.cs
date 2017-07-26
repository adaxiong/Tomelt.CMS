using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ArticleManage.Models;
using ArticleManage.Services;
using ArticleManage.ViewModels;
using Tomelt;
using Tomelt.ContentManagement;
using Tomelt.ContentManagement.Drivers;

namespace ArticleManage.Drivers
{
    public class ColumnPartDriver: ContentPartDriver<ColumnPart>
    {
        public ITomeltServices TomeltServices { get; set; }
        public IColumnService ColumnService { get; set; }
        private const string TemplateName = "Parts/Column";

        public ColumnPartDriver(ITomeltServices tomeltServices, IColumnService columnService)
        {
            TomeltServices = tomeltServices;
            ColumnService = columnService;
        }
        protected override DriverResult Display(ColumnPart part, string displayType, dynamic shapeHelper)
        {
            return ContentShape("Parts_Column", () => shapeHelper.Parts_Column);
        }
        protected override DriverResult Editor(ColumnPart part, dynamic shapeHelper)
        {
            return ContentShape("Parts_Column_Edit", () => shapeHelper.EditorTemplate(TemplateName: TemplateName, Model: part, Prefix: Prefix));
        }
        protected override DriverResult Editor(ColumnPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            var model = new EditColumnPartViewModel();
            updater.TryUpdateModel(model, Prefix, null, null);

            if (part.ContentItem.Id != 0)
            {
                ColumnService.UpdateForContentItem(part.ContentItem, model);
            }
            return Editor(part, shapeHelper);
        }
    }
}