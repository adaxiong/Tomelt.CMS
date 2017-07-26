using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tomelt.Environment.Extensions.Models;
using Tomelt.Security.Permissions;

namespace ArticleManage
{
    public class ArticleManagePermissions : IPermissionProvider
    {
        // Note - in code you should demand PublishContent, EditContent, or DeleteContent
        // Do not demand the "Own" variations - those are applied automatically when you demand the main ones

        public static readonly Permission PublishContent = new Permission { Description = "发布他人内容", Name = "PublishContent" };
        public static readonly Permission PublishOwnContent = new Permission { Description = "发布自己的内容", Name = "PublishOwnContent", ImpliedBy = new[] { PublishContent } };
        public static readonly Permission EditContent = new Permission { Description = "编辑他人内容", Name = "EditContent", ImpliedBy = new[] { PublishContent } };
        public static readonly Permission EditOwnContent = new Permission { Description = "编辑自己的内容", Name = "EditOwnContent", ImpliedBy = new[] { EditContent, PublishOwnContent } };
        public static readonly Permission DeleteContent = new Permission { Description = "删除他人内容", Name = "DeleteContent" };
        public static readonly Permission DeleteOwnContent = new Permission { Description = "删除自己的内容", Name = "DeleteOwnContent", ImpliedBy = new[] { DeleteContent } };
        public static readonly Permission ViewContent = new Permission { Description = "查看他人内容", Name = "ViewContent", ImpliedBy = new[] { EditContent } };
        public static readonly Permission ViewOwnContent = new Permission { Description = "查看自己内容", Name = "ViewOwnContent", ImpliedBy = new[] { ViewContent } };
        public static readonly Permission PreviewContent = new Permission { Description = "预览他人内容", Name = "PreviewContent", ImpliedBy = new[] { EditContent, PublishContent } };
        public static readonly Permission PreviewOwnContent = new Permission { Description = "预览自己的内容", Name = "PreviewOwnContent", ImpliedBy = new[] { PreviewContent } };


        public static readonly Permission MetaListContent = new Permission { ImpliedBy = new[] { EditOwnContent, PublishOwnContent, DeleteOwnContent } };

        public virtual Feature Feature { get; set; }

        public IEnumerable<Permission> GetPermissions()
        {
            return new[] {
                EditOwnContent,
                EditContent,
                PublishOwnContent,
                PublishContent,
                DeleteOwnContent,
                DeleteContent,
                ViewContent,
                ViewOwnContent,
                PreviewOwnContent,
                PreviewContent
            };
        }

        public IEnumerable<PermissionStereotype> GetDefaultStereotypes()
        {
            return new[] {
                new PermissionStereotype {
                    Name = "Administrator",
                    Permissions = new[] {PublishContent,EditContent,DeleteContent,PreviewContent}
                },
                new PermissionStereotype {
                    Name = "Editor",
                    Permissions = new[] {PublishContent,EditContent,DeleteContent,PreviewContent}
                },
                new PermissionStereotype {
                    Name = "Moderator"
                },
                new PermissionStereotype {
                    Name = "Author",
                    Permissions = new[] {PublishOwnContent,EditOwnContent,DeleteOwnContent,PreviewOwnContent}
                },
                new PermissionStereotype {
                    Name = "Contributor",
                    Permissions = new[] {EditOwnContent,PreviewOwnContent}
                },
                new PermissionStereotype {
                    Name = "Authenticated",
                    Permissions = new[] {ViewContent}
                },
                new PermissionStereotype {
                    Name = "Anonymous",
                    Permissions = new[] {ViewContent}
                },
            };
        }
    }
}