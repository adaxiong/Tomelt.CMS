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
    public class ArticlePartDriver: ContentPartDriver<ArticlePart>
    {
        public ITomeltServices TomeltServices { get; set; }
        public IArticleService ArticleService { get; set; }
        private const string TemplateName = "Parts/Article";
        public ArticlePartDriver(ITomeltServices tomeltServices, IArticleService articleService)
        {
            TomeltServices = tomeltServices;
            ArticleService = articleService;
        }

        protected override DriverResult Display(ArticlePart part, string displayType, dynamic shapeHelper)
        {
            return ContentShape("Parts_Article", () => shapeHelper.Parts_Article);
        }

        protected override DriverResult Editor(ArticlePart part, dynamic shapeHelper)
        {
            return ContentShape("Parts_Article_Edit", () => shapeHelper.EditorTemplate(TemplateName: TemplateName, Model: part, Prefix: Prefix));
        }

        protected override DriverResult Editor(ArticlePart part, IUpdateModel updater, dynamic shapeHelper)
        {
            var viewModel=new EditArticlePartViewModel();
            updater.TryUpdateModel(viewModel, Prefix, null, null);
            if (part.ContentItem.Id!=0)
            {
                ArticleService.UpdateForContentItem(part.ContentItem,viewModel);
            }
            return Editor(part, shapeHelper);
        }

        //private EditArticlePartViewModel BuildEditorViewModel(ArticlePart part)
        //{
        //    return new EditArticlePartViewModel()
        //    {
        //        Title = part.Title,

        //    };
        //}
    }
}