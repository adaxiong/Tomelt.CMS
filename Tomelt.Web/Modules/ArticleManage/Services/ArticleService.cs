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
using Tomelt.Core.Title.Models;
using Tomelt.Data;
using Tomelt.Settings;
using Tomelt.UI.Navigation;

namespace ArticleManage.Services
{
    public class ArticleService : IArticleService
    {
        private const string ContentTypeName = "Article";
        public IRepository<ColumnPartRecord> ColumnRepository { get; set; }
        public IRepository<ArticlePartRecord> ArticleRepository { get; set; }
        public ITomeltServices TomeltServices { get; set; }
        public IContentDefinitionManager ContentDefinitionManager { get; set; }
        public ISiteService SiteService { get; set; }
        public ArticleService(ITomeltServices tomeltServices,
            IRepository<ColumnPartRecord> columnRepository, 
            IContentDefinitionManager contentDefinitionManager,
            ISiteService siteService,
            IRepository<ArticlePartRecord> articleRepository)
        {
            TomeltServices = tomeltServices;
            ColumnRepository = columnRepository;
            ContentDefinitionManager = contentDefinitionManager;
            SiteService = siteService;
            ArticleRepository = articleRepository;
        }
        public IContentQuery<ContentItem> GetArticles(VersionOptions versionOptions)
        {
            return TomeltServices.ContentManager.Query(versionOptions, ContentTypeName);

        }

        public void UpdateForContentItem(ContentItem item)
        {
            var articlePart = item.As<ArticlePart>();
            var bodyPart = item.As<BodyPart>();
            if (bodyPart!=null)
            {
                articlePart.Summary = string.IsNullOrWhiteSpace(articlePart.Summary) ? Utity.DropHtml(bodyPart.Text, 255) : articlePart.Summary;
            }
            articlePart.ColumnPartRecord = ColumnRepository.Get(d => d.Id == articlePart.ColumnPartRecordId);
            ArticleRepository.Flush();
        }

        public IEnumerable<ContentItem> GetArticlesPro(EditArticlePartViewModel search)
        {
            //内容状态
            VersionOptions versionOptions;
            switch (search.contentStatus)
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
            if (search.ColumnPartRecordId>0)
            {
                var columnIds = ColumnRepository.Fetch(d => d.TreePath.Contains("," + search.ColumnPartRecordId + ","))
                    .Select(d => d.Id).ToList();
                query = query.Where<ArticlePartRecord>(d=>columnIds.Contains(d.ColumnPartRecordId));
            }
            if (!string.IsNullOrWhiteSpace(search.Title))
            {
                query = query.Where<TitlePartRecord>(d => d.Title.Contains(search.Title));
            }


            //升降序
            switch (search.order)
            {
                case "asc":
                    query = query.OrderBy<CommonPartRecord>(cr => cr.CreatedUtc);
                    break;
                default:
                    query = query.OrderByDescending<CommonPartRecord>(cr => cr.CreatedUtc);
                    break;
            }
            //查看自己的数据
            if (search.contentStatus == "Owner")
            {
                query = query.Where<CommonPartRecord>(cr => cr.OwnerId == TomeltServices.WorkContext.CurrentUser.Id);
            }
            search.total = query.Count();
            //分页
            int pageSize = search.rows ?? 10;
            var maxPagedCount = SiteService.GetSiteSettings().MaxPagedCount;
            if (maxPagedCount > 0 && pageSize > maxPagedCount)
                pageSize = maxPagedCount;
            int page = search.page ?? 1;
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

        public int GetArticlesCountByColumnId(int columnId)
        {
            if (columnId <= 0) return columnId;
            var columnIds = ColumnRepository.Fetch(d => d.TreePath.Contains("," + columnId + ","))
                .Select(d => d.Id).ToList();
            return TomeltServices.ContentManager.Query(VersionOptions.Latest).Where<ArticlePartRecord>(d => columnIds.Contains(d.ColumnPartRecordId)).Count();
        }
    }
}