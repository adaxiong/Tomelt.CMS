using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using System.Web.Routing;
using Tomelt.ContentManagement;
using Tomelt.Core.Settings.Models;
using Tomelt.DisplayManagement;
using Tomelt.Localization;
using Tomelt.Mvc;
using Tomelt.Security;
using Tomelt.UI.Notify;
using Tomelt.Users.Events;
using Tomelt.Users.Models;
using Tomelt.Users.Services;
using Tomelt.Users.ViewModels;
using Tomelt.Mvc.Extensions;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Web.Mvc.Html;
using Tomelt.Mvc.AntiForgery;
using Tomelt.Settings;
using Tomelt.UI.Navigation;
using Tomelt.Utility.Extensions;

namespace Tomelt.Users.Controllers
{
    [ValidateInput(false)]
    public class AdminController : Controller, IUpdateModel
    {
        private readonly IMembershipService _membershipService;
        private readonly IUserService _userService;
        private readonly IUserEventHandler _userEventHandlers;
        private readonly ISiteService _siteService;

        public AdminController(
            ITomeltServices services,
            IMembershipService membershipService,
            IUserService userService,
            IShapeFactory shapeFactory,
            IUserEventHandler userEventHandlers,
            ISiteService siteService)
        {
            Services = services;
            _membershipService = membershipService;
            _userService = userService;
            _userEventHandlers = userEventHandlers;
            _siteService = siteService;

            T = NullLocalizer.Instance;
            Shape = shapeFactory;
        }

        dynamic Shape { get; set; }
        public ITomeltServices Services { get; set; }
        public Localizer T { get; set; }

        //public ActionResult Index(UserIndexOptions options, PagerParameters pagerParameters)
        //{
        //    if (!Services.Authorizer.Authorize(Permissions.ManageUsers, T("Not authorized to list users")))
        //        return new HttpUnauthorizedResult();

        //    var pager = new Pager(_siteService.GetSiteSettings(), pagerParameters);

        //    // default options
        //    if (options == null)
        //        options = new UserIndexOptions();

        //    var users = Services.ContentManager
        //        .Query<UserPart, UserPartRecord>();

        //    switch (options.Filter)
        //    {
        //        case UsersFilter.Approved:
        //            users = users.Where(u => u.RegistrationStatus == UserStatus.Approved);
        //            break;
        //        case UsersFilter.Pending:
        //            users = users.Where(u => u.RegistrationStatus == UserStatus.Pending);
        //            break;
        //        case UsersFilter.EmailPending:
        //            users = users.Where(u => u.EmailStatus == UserStatus.Pending);
        //            break;
        //    }

        //    if (!string.IsNullOrWhiteSpace(options.Search))
        //    {
        //        users = users.Where(u => u.UserName.Contains(options.Search) || u.Email.Contains(options.Search));
        //    }

        //    var pagerShape = Shape.Pager(pager).TotalItemCount(users.Count());

        //    switch (options.Order)
        //    {
        //        case UsersOrder.Name:
        //            users = users.OrderBy(u => u.UserName);
        //            break;
        //        case UsersOrder.Email:
        //            users = users.OrderBy(u => u.Email);
        //            break;
        //        case UsersOrder.CreatedUtc:
        //            users = users.OrderBy(u => u.CreatedUtc);
        //            break;
        //        case UsersOrder.LastLoginUtc:
        //            users = users.OrderBy(u => u.LastLoginUtc);
        //            break;
        //    }

        //    var results = users
        //        .Slice(pager.GetStartIndex(), pager.PageSize)
        //        .ToList();

        //    var model = new UsersIndexViewModel
        //    {
        //        Users = results
        //            .Select(x => new UserEntry { User = x.Record })
        //            .ToList(),
        //        Options = options,
        //        Pager = pagerShape
        //    };

        //    // maintain previous route data when generating page links
        //    var routeData = new RouteData();
        //    routeData.Values.Add("Options.Filter", options.Filter);
        //    routeData.Values.Add("Options.Search", options.Search);
        //    routeData.Values.Add("Options.Order", options.Order);

        //    pagerShape.RouteData(routeData);

        //    return View(model);
        //}
        public ActionResult Index()
        {
            //获取当前内容类型
            var part= Services.ContentManager.New("User").Parts.FirstOrDefault(d => d.PartDefinition.Name == "User");
            IDictionary<string,string> dc=new Dictionary<string, string>();
            //获取用户类型字段属性名称和显示名称
            var record = typeof(UserPartRecord);
            foreach (var propertyInfo in record.GetProperties())
            {
                var typeName = propertyInfo.Name;
                var displayName = typeName;
                var displayAttr = propertyInfo.GetCustomAttribute<DisplayAttribute>();
                if (!string.IsNullOrWhiteSpace(displayAttr?.Name))
                {
                    displayName = displayAttr.Name;
                }

                dc.Add(typeName,displayName);

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
        public ActionResult GetList(UsersIndexViewModel search)
        {
            if (!Services.Authorizer.Authorize(Permissions.ManageUsers, T("无权限查看用户列表")))
                return new HttpUnauthorizedResult();
            var users = Services.ContentManager.Query<UserPart, UserPartRecord>();
            if (!string.IsNullOrWhiteSpace(search.UserNameOrEmali))
            {
                users = users.Where(u => u.UserName.Contains(search.UserNameOrEmali) || u.Email.Contains(search.UserNameOrEmali));
            }
            search.total = users.Count();
            int page = search.page ?? 1;
            int rows = search.rows ?? 10;
            string order = string.IsNullOrWhiteSpace(search.order) ? "desc" : search.order;
            var list = order == "desc" ? users.OrderByDescending(d => d.Id).Slice((page - 1) * rows, rows).ToList() : users.OrderBy(d => d.Id).Slice((page - 1) * rows, rows).ToList();
            return Json(new
            {
                search.total,
                rows = list.Select(d => new
                {
                    d.UserName,
                    d.Email,
                    d.CreatedUtc,
                    d.LastLoginUtc,
                    d.RegistrationStatus,
                    d.Id
                })
                
            });
        }
        [HttpPost]
        [FormValueRequired("submit.BulkEdit")]
        public ActionResult Index(FormCollection input)
        {
            if (!Services.Authorizer.Authorize(Permissions.ManageUsers, T("Not authorized to manage users")))
                return new HttpUnauthorizedResult();

            var viewModel = new UsersIndexViewModel { Users = new List<UserEntry>(), Options = new UserIndexOptions() };
            UpdateModel(viewModel);

            var checkedEntries = viewModel.Users.Where(c => c.IsChecked);
            switch (viewModel.Options.BulkAction)
            {
                case UsersBulkAction.None:
                    break;
                case UsersBulkAction.Approve:
                    foreach (var entry in checkedEntries)
                    {
                        Approve(entry.User.Id);
                    }
                    break;
                case UsersBulkAction.Disable:
                    foreach (var entry in checkedEntries)
                    {
                        Moderate(entry.User.Id);
                    }
                    break;
                case UsersBulkAction.ChallengeEmail:
                    foreach (var entry in checkedEntries)
                    {
                        SendChallengeEmail(entry.User.Id);
                    }
                    break;
                case UsersBulkAction.Delete:
                    foreach (var entry in checkedEntries)
                    {
                        Delete(entry.User.Id);
                    }
                    break;
            }

            return RedirectToAction("Index", ControllerContext.RouteData.Values);
        }

        public ActionResult Create()
        {
            if (!Services.Authorizer.Authorize(Permissions.ManageUsers, T("Not authorized to manage users")))
                return new HttpUnauthorizedResult();

            var user = Services.ContentManager.New<IUser>("User");
            var editor = Shape.EditorTemplate(TemplateName: "Parts/User.Create", Model: new UserCreateViewModel(), Prefix: null);
            editor.Metadata.Position = "2";
            var model = Services.ContentManager.BuildEditor(user);
            model.Content.Add(editor);

            return View(model);
        }

        [HttpPost, ActionName("Create")]
        public ActionResult CreatePOST(UserCreateViewModel createModel)
        {
            if (!Services.Authorizer.Authorize(Permissions.ManageUsers, T("Not authorized to manage users")))
                return new HttpUnauthorizedResult();

            if (!string.IsNullOrEmpty(createModel.UserName))
            {
                if (!_userService.VerifyUserUnicity(createModel.UserName, createModel.Email))
                {
                    AddModelError("NotUniqueUserName", T("User with that username and/or email already exists."));
                }
            }

            if (!Regex.IsMatch(createModel.Email ?? "", UserPart.EmailPattern, RegexOptions.IgnoreCase))
            {
                // http://haacked.com/archive/2007/08/21/i-knew-how-to-validate-an-email-address-until-i.aspx    
                ModelState.AddModelError("Email", T("You must specify a valid email address."));
            }

            if (createModel.Password != createModel.ConfirmPassword)
            {
                AddModelError("ConfirmPassword", T("Password confirmation must match"));
            }

            var user = Services.ContentManager.New<IUser>("User");
            if (ModelState.IsValid)
            {
                user = _membershipService.CreateUser(new CreateUserParams(
                                                  createModel.UserName,
                                                  createModel.Password,
                                                  createModel.Email,
                                                  null, null, true));
            }

            var model = Services.ContentManager.UpdateEditor(user, this);

            if (!ModelState.IsValid)
            {
                Services.TransactionManager.Cancel();

                var editor = Shape.EditorTemplate(TemplateName: "Parts/User.Create", Model: createModel, Prefix: null);
                editor.Metadata.Position = "2";
                model.Content.Add(editor);

                return View(model);
            }

            Services.Notifier.Information(T("User created"));
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult CreateAJAX(UserCreateViewModel createModel)
        {
            if (!Services.Authorizer.Authorize(Permissions.ManageUsers, T("Not authorized to manage users")))
                return Json(new{State=0,Msg="无权限执行该操作"});

            if (!string.IsNullOrEmpty(createModel.UserName))
            {
                if (!_userService.VerifyUserUnicity(createModel.UserName, createModel.Email))
                {
                    return Json(new { State = 0, Msg = "用户名或电子邮箱已存在" });
                }
            }

            if (!Regex.IsMatch(createModel.Email ?? "", UserPart.EmailPattern, RegexOptions.IgnoreCase))
            {
                return Json(new { State = 0, Msg = "请输入正确的电子邮箱格式" });
            }

            if (createModel.Password != createModel.ConfirmPassword)
            {
                return Json(new { State = 0, Msg = "两次密码不匹配" });
            }

            Services.ContentManager.New<IUser>("User");
            if (ModelState.IsValid)
            {
                _membershipService.CreateUser(new CreateUserParams(
                    createModel.UserName,
                    createModel.Password,
                    createModel.Email,
                    null, null, true));
            }

            if (!ModelState.IsValid)
            {
                Services.TransactionManager.Cancel();
                return Json(new { State = 0, Msg = "数据校验有误，登陆密码至少7位" });
            }
            return Json(new { State = 1, Msg = "创建用户成功" });
        }




        public ActionResult Edit(int id)
        {
            if (!Services.Authorizer.Authorize(Permissions.ManageUsers, T("Not authorized to manage users")))
                return new HttpUnauthorizedResult();

            var user = Services.ContentManager.Get<UserPart>(id);

            if (user == null)
                return HttpNotFound();

            var editor = Shape.EditorTemplate(TemplateName: "Parts/User.Edit", Model: new UserEditViewModel { User = user }, Prefix: null);
            editor.Metadata.Position = "2";
            var model = Services.ContentManager.BuildEditor(user);
            model.Content.Add(editor);

            return View(model);
        }

        [HttpPost]
        public ActionResult EditAJAX(int id)
        {
            if (!Services.Authorizer.Authorize(Permissions.ManageUsers, T("Not authorized to manage users")))
                return Json(new { State = 0, Msg = "无权限执行此操作" });

            var user = Services.ContentManager.Get<UserPart>(id, VersionOptions.DraftRequired);

            if (user == null)
                return Json(new { State = 0, Msg = "用户名不存在" });

            string previousName = user.UserName;
            Services.ContentManager.UpdateEditor(user, this);

            var editModel = new UserEditViewModel { User = user };
            if (TryUpdateModel(editModel))
            {
                if (!_userService.VerifyUserUnicity(id, editModel.UserName, editModel.Email))
                {
                    Services.TransactionManager.Cancel();
                    return Json(new { State = 0, Msg = "用户名或电子邮箱已存在" });
                }
                if (!Regex.IsMatch(editModel.Email ?? "", UserPart.EmailPattern, RegexOptions.IgnoreCase))
                {
                    Services.TransactionManager.Cancel();
                    return Json(new { State = 0, Msg = "电子邮箱格式不正确" });
                }
                // also update the Super user if this is the renamed account
                if (string.Equals(Services.WorkContext.CurrentSite.SuperUser, previousName, StringComparison.Ordinal))
                {
                    _siteService.GetSiteSettings().As<SiteSettingsPart>().SuperUser = editModel.UserName;
                }

                user.NormalizedUserName = editModel.UserName.ToLowerInvariant();
            }

            if (!ModelState.IsValid)
            {
                Services.TransactionManager.Cancel();
                return Json(new { State = 0, Msg = "数据校验不正确" });
            }

            Services.ContentManager.Publish(user.ContentItem);
            return Json(new { State = 1, Msg = "编辑成功" });
        }

        [HttpPost, ActionName("Edit")]
        public ActionResult EditPOST(int id)
        {
            if (!Services.Authorizer.Authorize(Permissions.ManageUsers, T("Not authorized to manage users")))
                return new HttpUnauthorizedResult();

            var user = Services.ContentManager.Get<UserPart>(id, VersionOptions.DraftRequired);

            if (user == null)
                return HttpNotFound();

            string previousName = user.UserName;

            var model = Services.ContentManager.UpdateEditor(user, this);

            var editModel = new UserEditViewModel { User = user };
            if (TryUpdateModel(editModel))
            {
                if (!_userService.VerifyUserUnicity(id, editModel.UserName, editModel.Email))
                {
                    AddModelError("NotUniqueUserName", T("User with that username and/or email already exists."));
                }
                else if (!Regex.IsMatch(editModel.Email ?? "", UserPart.EmailPattern, RegexOptions.IgnoreCase))
                {
                    // http://haacked.com/archive/2007/08/21/i-knew-how-to-validate-an-email-address-until-i.aspx    
                    ModelState.AddModelError("Email", T("You must specify a valid email address."));
                }
                else
                {
                    // also update the Super user if this is the renamed account
                    if (string.Equals(Services.WorkContext.CurrentSite.SuperUser, previousName, StringComparison.Ordinal))
                    {
                        _siteService.GetSiteSettings().As<SiteSettingsPart>().SuperUser = editModel.UserName;
                    }

                    user.NormalizedUserName = editModel.UserName.ToLowerInvariant();
                }
            }

            if (!ModelState.IsValid)
            {
                Services.TransactionManager.Cancel();

                var editor = Shape.EditorTemplate(TemplateName: "Parts/User.Edit", Model: editModel, Prefix: null);
                editor.Metadata.Position = "2";
                model.Content.Add(editor);

                return View(model);
            }

            Services.ContentManager.Publish(user.ContentItem);

            Services.Notifier.Information(T("User information updated"));
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (!Services.Authorizer.Authorize(Permissions.ManageUsers, T("Not authorized to manage users")))
                return new HttpUnauthorizedResult();

            var user = Services.ContentManager.Get<IUser>(id);

            if (user == null)
                return HttpNotFound();

            if (string.Equals(Services.WorkContext.CurrentSite.SuperUser, user.UserName, StringComparison.Ordinal))
            {
                Services.Notifier.Error(T("The Super user can't be removed. Please disable this account or specify another Super user account."));
            }
            else if (string.Equals(Services.WorkContext.CurrentUser.UserName, user.UserName, StringComparison.Ordinal))
            {
                Services.Notifier.Error(T("You can't remove your own account. Please log in with another account."));
            }
            else
            {
                Services.ContentManager.Remove(user.ContentItem);
                Services.Notifier.Information(T("User {0} deleted", user.UserName));
            }

            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryTokenTomelt(false)]
        public ActionResult DeleteAJAX(int id)
        {
            if (!Services.Authorizer.Authorize(Permissions.ManageUsers, T("Not authorized to manage users")))
                return Json(new { State = 0, Msg = "无权限执行此操作" });

            var user = Services.ContentManager.Get<IUser>(id);

            if (user == null)
                return Json(new { State = 0, Msg = "用户不存在" });

            if (string.Equals(Services.WorkContext.CurrentSite.SuperUser, user.UserName, StringComparison.Ordinal))
            {
                return Json(new { State = 0, Msg = "超级用户不能被删除。要禁用此帐户请登陆另一个超级用户帐户。" });
            }
            if (string.Equals(Services.WorkContext.CurrentUser.UserName, user.UserName, StringComparison.Ordinal))
            {
                return Json(new { State = 0, Msg = "自己的账户不能被删除。请登陆其他账户进行操作。" });
            }
            Services.ContentManager.Remove(user.ContentItem);
            return Json(new { State = 1, Msg = "删除成功" });
        }
        [HttpPost]
        public ActionResult SendChallengeEmail(int id)
        {
            if (!Services.Authorizer.Authorize(Permissions.ManageUsers, T("Not authorized to manage users")))
                return new HttpUnauthorizedResult();

            var user = Services.ContentManager.Get<IUser>(id);

            if (user == null)
                return HttpNotFound();

            var siteUrl = Services.WorkContext.CurrentSite.BaseUrl;

            if (string.IsNullOrWhiteSpace(siteUrl))
            {
                siteUrl = HttpContext.Request.ToRootUrlString();
            }

            _userService.SendChallengeEmail(user.As<UserPart>(), nonce => Url.MakeAbsolute(Url.Action("ChallengeEmail", "Account", new { Area = "Tomelt.Users", nonce = nonce }), siteUrl));
            Services.Notifier.Information(T("Challenge email sent to {0}", user.UserName));


            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Approve(int id)
        {
            if (!Services.Authorizer.Authorize(Permissions.ManageUsers, T("Not authorized to manage users")))
                return new HttpUnauthorizedResult();

            var user = Services.ContentManager.Get<IUser>(id);

            if (user == null)
                return HttpNotFound();

            user.As<UserPart>().RegistrationStatus = UserStatus.Approved;
            Services.Notifier.Information(T("User {0} approved", user.UserName));
            _userEventHandlers.Approved(user);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Moderate(int id)
        {
            if (!Services.Authorizer.Authorize(Permissions.ManageUsers, T("Not authorized to manage users")))
                return new HttpUnauthorizedResult();

            var user = Services.ContentManager.Get<IUser>(id);

            if (user == null)
                return HttpNotFound();

            if (string.Equals(Services.WorkContext.CurrentUser.UserName, user.UserName, StringComparison.Ordinal))
            {
                Services.Notifier.Error(T("You can't disable your own account. Please log in with another account"));
            }
            else
            {
                user.As<UserPart>().RegistrationStatus = UserStatus.Pending;
                Services.Notifier.Information(T("User {0} disabled", user.UserName));
            }

            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryTokenTomelt(false)]
        public ActionResult ModerateAJAX(int id)
        {
            if (!Services.Authorizer.Authorize(Permissions.ManageUsers, T("Not authorized to manage users")))
                return Json(new { State = 0, Msg = "无权限执行此操作" });

            var user = Services.ContentManager.Get<IUser>(id);

            if (user == null)
                return Json(new { State = 0, Msg = "用户不存在" });

            if (string.Equals(Services.WorkContext.CurrentUser.UserName, user.UserName, StringComparison.Ordinal))
            {
                return Json(new { State = 0, Msg = "不能禁用自己的用户，请登陆其他用户进行操作" });
            }
            user.As<UserPart>().RegistrationStatus = UserStatus.Pending;
            
            return Json(new { State = 1, Msg = "操作成功" });
        }
        [HttpPost]
        [ValidateAntiForgeryTokenTomelt(false)]
        public ActionResult ApproveAJAX(int id)
        {
            if (!Services.Authorizer.Authorize(Permissions.ManageUsers, T("Not authorized to manage users")))
                return Json(new { State = 0, Msg = "无权限执行此操作" });

            var user = Services.ContentManager.Get<IUser>(id);

            if (user == null)
                return Json(new { State = 0, Msg = "用户不存在" });

            user.As<UserPart>().RegistrationStatus = UserStatus.Approved;
            _userEventHandlers.Approved(user);
            return Json(new { State = 1, Msg = "操作成功" });
        }
        bool IUpdateModel.TryUpdateModel<TModel>(TModel model, string prefix, string[] includeProperties, string[] excludeProperties)
        {
            return TryUpdateModel(model, prefix, includeProperties, excludeProperties);
        }

        public void AddModelError(string key, LocalizedString errorMessage)
        {
            ModelState.AddModelError(key, errorMessage.ToString());
        }
    }

}
