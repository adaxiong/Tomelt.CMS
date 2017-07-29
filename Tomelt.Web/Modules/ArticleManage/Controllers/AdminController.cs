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
using Tomelt.ContentManagement.Aspects;
using Tomelt.ContentManagement.MetaData;
using Tomelt.ContentManagement.MetaData.Models;
using Tomelt.Core.Common.Models;
using Tomelt.Core.Contents.Settings;
using Tomelt.Core.Contents.ViewModels;
using Tomelt.Core.Title.Models;
using Tomelt.Data;
using Tomelt.Localization;
using Tomelt.Mvc;
using Tomelt.Mvc.AntiForgery;
using Tomelt.Mvc.Extensions;
using Tomelt.UI.Navigation;

namespace ArticleManage.Controllers
{
    [ValidateInput(false)]
    public class AdminController : Controller, IUpdateModel
    {
        public ITomeltServices TomeltServices { get; set; }
        public IArticleService ArticleService { get; set; }
        public IColumnService ColumnService { get; set; }
        public IContentDefinitionManager ContentDefinitionManager { get; set; }

        public ITransactionManager TransactionManager { get; set; }
        public Localizer T { get; set; }
        private const string ContentTypeName = "Article";

        public AdminController(ITomeltServices tomeltServices,
            IContentDefinitionManager contentDefinitionManager,
            IArticleService articleService, 
            IColumnService columnService, 
            ITransactionManager transactionManager)
        {
            TomeltServices = tomeltServices;
            ContentDefinitionManager = contentDefinitionManager;
            ArticleService = articleService;
            ColumnService = columnService;
            TransactionManager = transactionManager;
        }
        // GET: Admin
        public ActionResult Index()
        {
            if (!TomeltServices.Authorizer.Authorize(ArticleManagePermissions.ViewContent, T("无权限")))
                return new HttpUnauthorizedResult();
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
            if (!TomeltServices.Authorizer.Authorize(ArticleManagePermissions.ViewContent, T("无权限")))
                return new HttpUnauthorizedResult();
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
            if (!TomeltServices.Authorizer.Authorize(ArticleManagePermissions.ViewContent, T("无权限")))
                return new HttpUnauthorizedResult();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryTokenTomelt(false)]
        public ActionResult GetColumnList()
        {
            if (!TomeltServices.Authorizer.Authorize(ArticleManagePermissions.ViewContent, T("无权限")))
                return new HttpUnauthorizedResult();
            var rows = ColumnService.GetColumns(VersionOptions.Latest).OrderBy<ColumnPartRecord>(d=>d.Sort).List();
            return Json(new
            {
                total = rows.Count(),
                rows = rows.Select(d => new
                {
                    d.Id,
                    d.As<ColumnPart>().ParentId,
                    d.As<ColumnPart>().Sort,
                    d.As<TitlePart>().Title,
                    Count = GetArticleCount(d.Id),
                    _parentId = d.As<ColumnPart>().ParentId==0?null:d.As<ColumnPart>().ParentId.ToString()
                })

            });
        }
        /// <summary>
        /// 栏目树状数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryTokenTomelt(false)]
        public ActionResult GetColumnTreeList()
        {
            if (!TomeltServices.Authorizer.Authorize(ArticleManagePermissions.ViewContent, T("无权限")))
                return new HttpUnauthorizedResult();
            return Json(ColumnService.GetTreeColumns(VersionOptions.Latest));

        }
        /// <summary>
        /// 新增文章
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateArticle()
        {
            var contentItem = TomeltServices.ContentManager.New("Article");
            if (!TomeltServices.Authorizer.Authorize(ArticleManagePermissions.EditContent, contentItem, T("无权限")))
                return new HttpUnauthorizedResult();
            var model = TomeltServices.ContentManager.BuildEditor(contentItem);
            return View(model);
        }
        /// <summary>
        /// 编辑文章
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EditArticle(int id)
        {
            var contentItem = TomeltServices.ContentManager.Get(id, VersionOptions.Latest);
            if (contentItem == null)
                return HttpNotFound();
            if (!TomeltServices.Authorizer.Authorize(ArticleManagePermissions.EditContent, contentItem, T("无权限")))
                return new HttpUnauthorizedResult();
            var model = TomeltServices.ContentManager.BuildEditor(contentItem);
            return View(model);
        }
        /// <summary>
        /// 新增栏目
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateColumn()
        {
            var contentItem = TomeltServices.ContentManager.New("Column");
            if (!TomeltServices.Authorizer.Authorize(ArticleManagePermissions.EditContent, contentItem, T("无权限")))
                return new HttpUnauthorizedResult();
            var model = TomeltServices.ContentManager.BuildEditor(contentItem);
            return View(model);
        }
        /// <summary>
        /// 编辑栏目
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EditColumn(int id)
        {
            var contentItem = TomeltServices.ContentManager.Get(id, VersionOptions.Latest);
            if (contentItem == null)
                return HttpNotFound();
            if (!TomeltServices.Authorizer.Authorize(ArticleManagePermissions.EditContent, contentItem, T("无权限")))
                return new HttpUnauthorizedResult();
            var model = TomeltServices.ContentManager.BuildEditor(contentItem);
            return View(model);
        }














        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        [HttpPost, ActionName("Create")]
        [FormValueRequired("submit.Save")]
        public ActionResult CreateSave(string id)
        {
            //根据配置参数判断是否邮发布的设置
            return CreatePost(id, contentItem => {
                if (!contentItem.Has<IPublishingControlAspect>() && !contentItem.TypeDefinition.Settings.GetModel<ContentTypeSettings>().Draftable)
                    TomeltServices.ContentManager.Publish(contentItem);
            });
        }
        /// <summary>
        /// 创建并发布
        /// </summary>
        /// <returns></returns>
        [HttpPost, ActionName("Create")]
        [FormValueRequired("submit.Publish")]
        public ActionResult CreatePublish(string id)
        {
            var dummyContent = TomeltServices.ContentManager.New(id);

            if (!TomeltServices.Authorizer.Authorize(ArticleManagePermissions.PublishContent, dummyContent, T("Couldn't create content")))
                return Json(new { State = 0, Msg = T("无发布权限").Text });

            return CreatePost(id, contentItem => TomeltServices.ContentManager.Publish(contentItem));
        }


        /// <summary>
        /// 提交编辑内容项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Edit")]
        [FormValueRequired("submit.Save")]
        public ActionResult EditSave(int id)
        {
            return EditPost(id,contentItem => {
                if (!contentItem.Has<IPublishingControlAspect>() && !contentItem.TypeDefinition.Settings.GetModel<ContentTypeSettings>().Draftable)
                    TomeltServices.ContentManager.Publish(contentItem);
            });
        }
        /// <summary>
        /// 发布编辑内容项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Edit")]
        [FormValueRequired("submit.Publish")]
        public ActionResult EditPublish(int id)
        {
            var content = TomeltServices.ContentManager.Get(id, VersionOptions.Latest);

            if (content == null)
                return Json(new { State = 0, Msg = T("此内容不存在").Text });

            if (!TomeltServices.Authorizer.Authorize(ArticleManagePermissions.PublishContent, content, T("无发布权限")))
                return Json(new { State = 0, Msg = T("无发布权限").Text });

            return EditPost(id, contentItem => TomeltServices.ContentManager.Publish(contentItem));
        }
       


























        private ActionResult CreatePost(string typeId,Action<ContentItem> conditionallyPublish)
        {
            var contentItem = TomeltServices.ContentManager.New(typeId);

            if (!TomeltServices.Authorizer.Authorize(ArticleManagePermissions.EditContent, contentItem, T("无创建权限")))
                return Json(new{State=0,Msg= T("无创建权限").Text });

            TomeltServices.ContentManager.Create(contentItem, VersionOptions.Draft);

            TomeltServices.ContentManager.UpdateEditor(contentItem, this);

            if (!ModelState.IsValid)
            {
                TransactionManager.Cancel();
                return Json(new { State = 0, Msg = T("数据校验失败，请核实输入的数据是否正确").Text });
            }

            conditionallyPublish(contentItem);
            return Json(new { State = 1, Msg = T("创建成功").Text });
        }
        /// <summary>
        /// 公共部分编辑
        /// </summary>
        /// <returns></returns>
        private ActionResult EditPost(int id,Action<ContentItem> conditionallyPublish)
        {
            var contentItem = TomeltServices.ContentManager.Get(id, VersionOptions.DraftRequired);

            if (contentItem == null)
                return Json(new { State = 0, Msg = T("此内容不存在").Text });

            if (!TomeltServices.Authorizer.Authorize(ArticleManagePermissions.EditContent, contentItem, T("无编辑权限")))
                return Json(new { State = 0, Msg = T("无编辑权限").Text });

            //string previousRoute = null;
            //if (contentItem.Has<IAliasAspect>()
            //    && !string.IsNullOrWhiteSpace(returnUrl)
            //    && Request.IsLocalUrl(returnUrl)
            //    // only if the original returnUrl is the content itself
            //    && String.Equals(returnUrl, Url.ItemDisplayUrl(contentItem), StringComparison.OrdinalIgnoreCase)
            //)
            //{
            //    previousRoute = contentItem.As<IAliasAspect>().Path;
            //}

            TomeltServices.ContentManager.UpdateEditor(contentItem, this);
            if (!ModelState.IsValid)
            {
                TransactionManager.Cancel();
                return Json(new { State = 0, Msg = T("数据校验失败，请核实输入的数据是否正确").Text });
            }

            conditionallyPublish(contentItem);
            return Json(new { State = 1, Msg = T("编辑成功").Text });
        }

        /// <summary>
        /// 克隆栏目
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryTokenTomelt(false)]
        public ActionResult Clone(int id)
        {
            var contentItem = TomeltServices.ContentManager.GetLatest(id);

            if (contentItem == null)
                return Json(new { State = 0, Msg = T("无此内容").Text });

            if (!TomeltServices.Authorizer.Authorize(ArticleManagePermissions.EditContent, contentItem, T("无复制权限")))
                return Json(new { State = 0, Msg = T("无复制权限").Text });
            //动作
            try
            {
                TomeltServices.ContentManager.Clone(contentItem);
            }
            catch (InvalidOperationException)
            {
                return Json(new { State = 0, Msg = T("复制失败").Text });
            }
            return Json(new { State = 1, Msg = T("复制成功").Text });
        }

        /// <summary>
        /// 发布内容
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryTokenTomelt(false)]
        public ActionResult Publish(int id)
        {
            var contentItem = TomeltServices.ContentManager.GetLatest(id);
            if (contentItem == null)
                return Json(new { State = 0, Msg = T("无此内容").Text });

            if (!TomeltServices.Authorizer.Authorize(ArticleManagePermissions.PublishContent, contentItem, T("无发布权限")))
                return Json(new { State = 0, Msg = T("无发布权限").Text });

            TomeltServices.ContentManager.Publish(contentItem);
            return Json(new { State = 1, Msg = T("发布成功").Text });

        }
        /// <summary>
        /// 取消发布内容
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryTokenTomelt(false)]
        public ActionResult Unpublish(int id)
        {
            var contentItem = TomeltServices.ContentManager.GetLatest(id);
            if (contentItem == null)
                return Json(new { State = 0, Msg = T("无此内容").Text });

            if (!TomeltServices.Authorizer.Authorize(ArticleManagePermissions.PublishContent, contentItem, T("无取消发布权限")))
                return Json(new { State = 0, Msg = T("无取消发布权限").Text });

            TomeltServices.ContentManager.Unpublish(contentItem);
            return Json(new { State = 1, Msg = T("取消发布成功").Text });

        }
        /// <summary>
        /// 删除内容
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryTokenTomelt(false)]
        public ActionResult Delete(int id)
        {
            var contentItem = TomeltServices.ContentManager.Get(id, VersionOptions.Latest);

            if (!TomeltServices.Authorizer.Authorize(ArticleManagePermissions.DeleteContent, contentItem, T("无删除权限")))
                return Json(new { State = 0, Msg = T("无删除权限").Text });

            if (contentItem != null)
            {
                TomeltServices.ContentManager.Remove(contentItem);
                return Json(new { State = 1, Msg = T("删除成功").Text });

            }
            return Json(new { State = 0, Msg = T("无此内容").Text });
        }










        private int GetArticleCount(int columnId)
        {
            var temp= ArticleService.GetArticles(VersionOptions.Latest).Where<ArticlePartRecord>(d=>d.ColumnPartRecordId==columnId);
            return temp.Count();
        }












        bool IUpdateModel.TryUpdateModel<TModel>(TModel model, string prefix, string[] includeProperties, string[] excludeProperties)
        {
            return TryUpdateModel(model, prefix, includeProperties, excludeProperties);
        }

        void IUpdateModel.AddModelError(string key, LocalizedString errorMessage)
        {
            ModelState.AddModelError(key, errorMessage.ToString());
        }
    }
}