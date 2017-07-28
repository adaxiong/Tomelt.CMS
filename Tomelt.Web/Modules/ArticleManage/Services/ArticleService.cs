using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using ArticleManage.Models;
using ArticleManage.ViewModels;
using Tomelt;
using Tomelt.ContentManagement;
using Tomelt.ContentManagement.MetaData;
using Tomelt.ContentManagement.MetaData.Models;
using Tomelt.ContentManagement.Records;
using Tomelt.Core.Common.Models;
using Tomelt.Core.Contents.Settings;
using Tomelt.Data;
using Tomelt.Settings;
using Tomelt.UI.Navigation;

namespace ArticleManage.Services
{
    public class ArticleService : IArticleService
    {
        private const string ContentTypeName = "Article";
        public IRepository<ColumnPartRecord> ColumnRepository { get; set; }
        public ITomeltServices TomeltServices { get; set; }
        public IContentDefinitionManager ContentDefinitionManager { get; set; }
        public ISiteService SiteService { get; set; }
        public ArticleService(ITomeltServices tomeltServices,
            IRepository<ColumnPartRecord> columnRepository, 
            IContentDefinitionManager contentDefinitionManager,
            ISiteService siteService)
        {
            TomeltServices = tomeltServices;
            ColumnRepository = columnRepository;
            ContentDefinitionManager = contentDefinitionManager;
            SiteService = siteService;
        }
        public IEnumerable<ContentItem> GetArticles<TRecord>(VersionOptions versionOptions, Expression<Func<TRecord, bool>> whereLambad) where TRecord:ContentPartRecord
        {
            return TomeltServices.ContentManager.Query(versionOptions, ContentTypeName).Where(whereLambad).List();

        }

        public void UpdateForContentItem(ContentItem item, EditArticlePartViewModel model)
        {
            var articlePart = item.As<ArticlePart>();
            var bodyPart = item.As<BodyPart>();
            if (bodyPart!=null)
            {
                articlePart.Summary = string.IsNullOrWhiteSpace(model.Summary) ? Utity.DropHtml(bodyPart.Text, 255) : model.Summary;
            }
            else
            {
                articlePart.Summary = model.Summary;
            }
            articlePart.Author = model.Author;
            articlePart.ClickNum = model.ClickNum;
            articlePart.IsHot = model.IsHot;
            articlePart.IsRecommend = model.IsRecommend;
            articlePart.IsSlide = model.IsSlide;
            articlePart.IsStriking = model.IsStriking;
            articlePart.IsTop = model.IsTop;
            articlePart.LinkUrl = model.LinkUrl;
            articlePart.Sort = model.Sort;
            articlePart.Source = model.Source;
            articlePart.Subtitle = model.Subtitle;
            articlePart.ColumnPartRecordId = model.ColumnPartRecordId;
            articlePart.ColumnPartRecord = ColumnRepository.Get(d => d.Id == model.ColumnPartRecordId);
        }

        public IEnumerable<ContentItem> GetArticlesPro(DatagridPagerParameters pagerParameters)
        {
            //内容状态
            VersionOptions versionOptions;
            switch (pagerParameters.contentStatus)
            {
                case "Published":
                    versionOptions = VersionOptions.Published;
                    break;
                case "Draft":
                    versionOptions = VersionOptions.Draft;
                    break;
                case "AllVersions":
                    versionOptions = VersionOptions.AllVersions;
                    break;
                default:
                    versionOptions = VersionOptions.Latest;
                    break;
            }
            var query = TomeltServices.ContentManager.Query(versionOptions, GetCreatableTypes(false).Select(ctd => ctd.Name).ToArray());
            query = query.ForType(ContentTypeName);
            //升降序
            switch (pagerParameters.order)
            {
                case "asc":
                    query = query.OrderBy<CommonPartRecord>(cr => cr.CreatedUtc);
                    break;
                default:
                    query = query.OrderByDescending<CommonPartRecord>(cr => cr.CreatedUtc);
                    break;
            }
            //查看自己的数据
            if (pagerParameters.contentStatus == "Owner")
            {
                query = query.Where<CommonPartRecord>(cr => cr.OwnerId == TomeltServices.WorkContext.CurrentUser.Id);
            }
            pagerParameters.total = query.Count();
            //分页
            int pageSize = pagerParameters.rows ?? 10;
            var maxPagedCount = SiteService.GetSiteSettings().MaxPagedCount;
            if (maxPagedCount > 0 && pageSize > maxPagedCount)
                pageSize = maxPagedCount;
            int page = pagerParameters.page ?? 1;
            return query.Slice((page - 1) * pageSize, pageSize).ToList();
        }
        private IEnumerable<ContentTypeDefinition> GetCreatableTypes(bool andContainable)
        {
            return ContentDefinitionManager.ListTypeDefinitions().Where(ctd =>
                TomeltServices.Authorizer.Authorize(ArticleManagePermissions.EditContent, TomeltServices.ContentManager.New(ctd.Name)) &&
                ctd.Name == ContentTypeName &&
                ctd.Settings.GetModel<ContentTypeSettings>().Creatable &&
                (!andContainable || ctd.Parts.Any(p => p.PartDefinition.Name == "ContainablePart")));
        }
    }
}