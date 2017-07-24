using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Tomelt.Localization;
using Tomelt.Logging;
using Tomelt.Mvc;
using Tomelt.Mvc.AntiForgery;
using Tomelt.Mvc.Extensions;
using Tomelt.Roles.Models;
using Tomelt.Roles.Services;
using Tomelt.Roles.ViewModels;
using Tomelt.Security;
using Tomelt.UI.Notify;

namespace Tomelt.Roles.Controllers
{
    [ValidateInput(false)]
    public class AdminController : Controller
    {
        private readonly IRoleService _roleService;
        private readonly IAuthorizationService _authorizationService;

        public AdminController(
            ITomeltServices services,
            IRoleService roleService,
            INotifier notifier,
            IAuthorizationService authorizationService)
        {
            Services = services;
            _roleService = roleService;
            _authorizationService = authorizationService;

            T = NullLocalizer.Instance;
            Logger = NullLogger.Instance;
        }

        public ITomeltServices Services { get; set; }
        public Localizer T { get; set; }
        public ILogger Logger { get; set; }

        public ActionResult Index()
        {
            if (!Services.Authorizer.Authorize(Permissions.ManageRoles, T("Not authorized to manage roles")))
                return new HttpUnauthorizedResult();

            var model = new RolesIndexViewModel { Rows = _roleService.GetRoles().OrderBy(r => r.Name).ToList() };

            return View(model);
        }
        public ActionResult List()
        {
            if (!Services.Authorizer.Authorize(Permissions.ManageRoles, T("无权限")))
                return new HttpUnauthorizedResult();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryTokenTomelt(false)]
        public ActionResult GetList()
        {
            if (!Services.Authorizer.Authorize(Permissions.ManageRoles, T("无权限")))
                return new HttpUnauthorizedResult();

            var rows = _roleService.GetRoles().OrderBy(r => r.Name).ToList();

            return Json(rows.Select(d => new
            {
                d.Name,
                d.Id
            }));
        }
        [HttpPost, ActionName("Index")]
        public ActionResult IndexPOST()
        {
            if (!Services.Authorizer.Authorize(Permissions.ManageRoles, T("Not authorized to manage roles")))
                return new HttpUnauthorizedResult();

            foreach (string key in Request.Form.Keys)
            {
                if (key.StartsWith("Checkbox.") && Request.Form[key] == "true")
                {
                    int roleId = Convert.ToInt32(key.Substring("Checkbox.".Length));
                    _roleService.DeleteRole(roleId);
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            if (!Services.Authorizer.Authorize(Permissions.ManageRoles, T("Not authorized to manage roles")))
                return new HttpUnauthorizedResult();

            var model = new RoleCreateViewModel { FeaturePermissions = _roleService.GetInstalledPermissions() };
            return View(model);
        }

        [HttpPost, ActionName("Create")]
        public ActionResult CreatePOST()
        {
            if (!Services.Authorizer.Authorize(Permissions.ManageRoles, T("Not authorized to manage roles")))
                return new HttpUnauthorizedResult();

            var viewModel = new RoleCreateViewModel();
            TryUpdateModel(viewModel);

            if (String.IsNullOrEmpty(viewModel.Name))
            {
                ModelState.AddModelError("Name", T("Role name can't be empty"));
            }

            var role = _roleService.GetRoleByName(viewModel.Name);
            if (role != null)
            {
                ModelState.AddModelError("Name", T("Role with same name already exists"));
            }

            if (!ModelState.IsValid)
            {
                viewModel.FeaturePermissions = _roleService.GetInstalledPermissions();
                return View(viewModel);
            }

            _roleService.CreateRole(viewModel.Name);
            foreach (string key in Request.Form.Keys)
            {
                if (key.StartsWith("Checkbox.") && Request.Form[key] == "true")
                {
                    string permissionName = key.Substring("Checkbox.".Length);
                    _roleService.CreatePermissionForRole(viewModel.Name,
                                                            permissionName);
                }
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult CreateAJAX()
        {
            if (!Services.Authorizer.Authorize(Permissions.ManageRoles, T("Not authorized to manage roles")))
                return Json(new { State = 0, Msg = T("无权限").Text });

            var viewModel = new RoleCreateViewModel();
            TryUpdateModel(viewModel);

            if (String.IsNullOrEmpty(viewModel.Name))
            {
                return Json(new { State = 0, Msg = T("角色名称不能为空").Text });
            }

            var role = _roleService.GetRoleByName(viewModel.Name);
            if (role != null)
            {
                return Json(new { State = 0, Msg = T("该角色名称已存在").Text });
            }

            if (!ModelState.IsValid)
            {
                viewModel.FeaturePermissions = _roleService.GetInstalledPermissions();
                return Json(new { State = 0, Msg = T("数据校验失败，请核实输入数据").Text });
            }

            _roleService.CreateRole(viewModel.Name);
            foreach (string key in Request.Form.Keys)
            {
                if (key.StartsWith("Checkbox.") && Request.Form[key] == "true")
                {
                    string permissionName = key.Substring("Checkbox.".Length);
                    _roleService.CreatePermissionForRole(viewModel.Name,
                        permissionName);
                }
            }
            return Json(new { State = 1, Msg = T("新增角色成功!").Text });
        }
        public ActionResult Edit(int id)
        {
            if (!Services.Authorizer.Authorize(Permissions.ManageRoles, T("Not authorized to manage roles")))
                return new HttpUnauthorizedResult();

            var role = _roleService.GetRole(id);
            if (role == null)
            {
                return HttpNotFound();
            }

            var model = new RoleEditViewModel
            {
                Name = role.Name,
                Id = role.Id,
                RoleCategoryPermissions = _roleService.GetInstalledPermissions(),
                CurrentPermissions = _roleService.GetPermissionsForRole(id)
            };

            var simulation = UserSimulation.Create(role.Name);
            model.EffectivePermissions = model.RoleCategoryPermissions
                .SelectMany(group => group.Value)
                .Where(permission => _authorizationService.TryCheckAccess(permission, simulation, null))
                .Select(permission => permission.Name)
                .Distinct()
                .ToList();

            return View(model);
        }

        [HttpPost, ActionName("Edit")]
        [FormValueRequired("submit.Save")]
        public ActionResult EditSavePOST(int id)
        {
            if (!Services.Authorizer.Authorize(Permissions.ManageRoles, T("Not authorized to manage roles")))
                return new HttpUnauthorizedResult();

            var viewModel = new RoleEditViewModel();
            TryUpdateModel(viewModel);

            if (String.IsNullOrEmpty(viewModel.Name))
            {
                ModelState.AddModelError("Name", T("Role name can't be empty"));
            }

            var role = _roleService.GetRoleByName(viewModel.Name);
            if (role != null && role.Id != id)
            {
                ModelState.AddModelError("Name", T("Role with same name already exists"));
            }

            if (!ModelState.IsValid)
            {
                return Edit(id);
            }

            // Save
            List<string> rolePermissions = new List<string>();
            foreach (string key in Request.Form.Keys)
            {
                if (key.StartsWith("Checkbox.") && Request.Form[key] == "true")
                {
                    string permissionName = key.Substring("Checkbox.".Length);
                    rolePermissions.Add(permissionName);
                }
            }
            _roleService.UpdateRole(viewModel.Id, viewModel.Name, rolePermissions);

            Services.Notifier.Information(T("Your Role has been saved."));
            return RedirectToAction("Edit", new { id });
        }
        [HttpPost]

        public ActionResult EditAJAX(int id)
        {
            if (!Services.Authorizer.Authorize(Permissions.ManageRoles, T("Not authorized to manage roles")))
                return Json(new { State = 0, Msg = T("无权限").Text });

            var viewModel = new RoleEditViewModel();
            TryUpdateModel(viewModel);

            if (String.IsNullOrEmpty(viewModel.Name))
            {
                return Json(new { State = 0, Msg = T("角色名称不能为空").Text });
            }

            var role = _roleService.GetRoleByName(viewModel.Name);
            if (role != null && role.Id != id)
            {
                return Json(new { State = 0, Msg = T("该角色名称已存在").Text });
            }

            if (!ModelState.IsValid)
            {
                return Json(new { State = 0, Msg = T("数据校验失败，请核实输入数据").Text });
            }

            // Save
            List<string> rolePermissions = new List<string>();
            foreach (string key in Request.Form.Keys)
            {
                if (key.StartsWith("Checkbox.") && Request.Form[key] == "true")
                {
                    string permissionName = key.Substring("Checkbox.".Length);
                    rolePermissions.Add(permissionName);
                }
            }
            _roleService.UpdateRole(viewModel.Id, viewModel.Name, rolePermissions);

            return Json(new { State = 1, Msg = T("修改角色成功").Text });
        }
        [HttpPost, ActionName("Edit")]
        [FormValueRequired("submit.Delete")]
        public ActionResult EditDeletePOST(int id)
        {
            return Delete(id, null);
        }

        [HttpPost]
        public ActionResult Delete(int id, string returnUrl)
        {
            if (!Services.Authorizer.Authorize(Permissions.ManageRoles, T("Not authorized to manage roles")))
                return new HttpUnauthorizedResult();

            _roleService.DeleteRole(id);
            Services.Notifier.Information(T("Role was successfully deleted."));

            return this.RedirectLocal(returnUrl, () => RedirectToAction("Index"));
        }
        [HttpPost]
        [ValidateAntiForgeryTokenTomelt(false)]
        public ActionResult DeleteAJAX(int id)
        {
            if (!Services.Authorizer.Authorize(Permissions.ManageRoles, T("Not authorized to manage roles")))
                return Json(new { State = 0, Msg = T("无权限").Text });
            var role = _roleService.GetRole(id);
            if (role != null && role.Name == "Administrator")
            {
                return Json(new { State = 0, Msg = T("不能删除此超级管理员角色").Text });
            }
            _roleService.DeleteRole(id);
            return Json(new { State = 1, Msg = T("成功删除角色").Text });
        }
    }
}
