using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using ArticleManage.Models;
using ArticleManage.Services;
using Tomelt;
using Tomelt.ContentManagement;
using Tomelt.ContentManagement.MetaData;
using Tomelt.ContentManagement.MetaData.Models;
using Tomelt.Core.Common.Models;
using Tomelt.Core.Contents.Settings;
using Tomelt.Core.Contents.ViewModels;
using Tomelt.Core.Title.Models;
using Tomelt.Mvc.AntiForgery;
using Tomelt.UI.Navigation;

namespace ArticleManage.Controllers
{
    public class AdminController : Controller
    {
        public ITomeltServices TomeltServices { get; set; }
        public IArticleService ArticleService { get; set; }
        public IColumnService ColumnService { get; set; }
        public IContentDefinitionManager ContentDefinitionManager { get; set; }

        private const string ContentTypeName = "Article";

        public AdminController(ITomeltServices tomeltServices,
            IContentDefinitionManager contentDefinitionManager,
            IArticleService articleService, IColumnService columnService)
        {
            TomeltServices = tomeltServices;
            ContentDefinitionManager = contentDefinitionManager;
            ArticleService = articleService;
            ColumnService = columnService;
        }
        // GET: Admin
        public ActionResult Index()
        {
            //获取当前内容类型
            var part = TomeltServices.ContentManager.New(ContentTypeName).Parts.FirstOrDefault(d => d.PartDefinition.Name == ContentTypeName);

            IDictionary<string, string> dc = new Dictionary<string, string>();
            //获取用户类型字段属性名称和显示名称
            var record = typeof(ArticlePartRecord);
            foreach (var propertyInfo in record.GetProperties())
            {
                var typeName = propertyInfo.Name;
                var displayName = typeName;
                var displayAttr = propertyInfo.GetCustomAttribute<DisplayAttribute>();
                if (!string.IsNullOrWhiteSpace(displayAttr?.Name))
                {
                    displayName = displayAttr.Name;
                }

                dc.Add(typeName, displayName);

            }
            //获取该内容类型的扩展字段
            if (part != null)
            {
                foreach (var contentField in part.Fields)
                {
                    dc.Add(contentField.Name, contentField.DisplayName);
                }
            }
            //去除不必要的字段
            if (dc.ContainsKey("ContentItemRecord"))
            {
                dc.Remove("ContentItemRecord");
            }
            ViewBag.Fields = dc;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryTokenTomelt(false)]
        public ActionResult GetList(DatagridPagerParameters pagerParameters)
        {

            var rows = ArticleService.GetArticlesPro(pagerParameters);
            return Json(new
            {
                pagerParameters.total,
                rows = rows.Select(d => new
                {
                    d.Id,
                    d.As<ArticlePart>().Author,
                    d.As<TitlePart>().Title,
                    d.As<CommonPart>().PublishedUtc,
                    UserName = d.As<CommonPart>().Owner == null ? "" : d.As<CommonPart>().Owner.UserName,
                    d.As<ArticlePart>().Sort
                })

            });
        }


        public ActionResult Column()
        {
            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryTokenTomelt(false)]
        public ActionResult GetColumnList()
        {

            var rows = ColumnService.GetColumns(VersionOptions.Latest);
            return Json(new
            {
                total = rows.Count(),
                rows = rows.Select(d => new
                {
                    d.Id,
                    d.As<ColumnPart>().Sort,
                    d.As<TitlePart>().Title,
                    Count = GetArticleCount(d.Id),
                    _parentId = d.As<ColumnPart>().ParentId
                })

            });
        }

        private int GetArticleCount(int columnId)
        {
            var temp= ArticleService.GetArticles<ArticlePartRecord>(VersionOptions.Latest, d => d.ColumnPartRecordId == columnId);
            return temp.Count();
        }
        [HttpPost]
        [ValidateAntiForgeryTokenTomelt(false)]
        public ActionResult GetColumnTreeList()
        {
            return Json(ColumnService.GetTreeColumns(VersionOptions.Latest));

        }
    }
}