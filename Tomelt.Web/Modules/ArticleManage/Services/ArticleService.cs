using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using ArticleManage.Models;
using ArticleManage.ViewModels;
using Tomelt;
using Tomelt.ContentManagement;
using Tomelt.Core.Common.Models;
using Tomelt.Data;

namespace ArticleManage.Services
{
    public class ArticleService : IArticleService
    {
        public IRepository<ColumnPartRecord> ColumnRepository { get; set; }
        public ITomeltServices TomeltServices { get; set; }
        public ArticleService(ITomeltServices tomeltServices, IRepository<ColumnPartRecord> columnRepository)
        {
            TomeltServices = tomeltServices;
            ColumnRepository = columnRepository;
        }
        public IEnumerable<ContentItem> GetArticles(VersionOptions versionOptions)
        {
            return TomeltServices.ContentManager.Query(versionOptions, "Article").List();

        }

        public void UpdateForContentItem(ContentItem item, EditArticlePartViewModel model)
        {
            var articlePart = item.As<ArticlePart>();
            var bodyPart = item.As<BodyPart>();
            articlePart.Title = model.Title;
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
    }
}