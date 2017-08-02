using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tomelt.Localization;
using Tomelt.UI.Navigation;

namespace ArticleManage
{
    public class AdminMenu : INavigationProvider
    {
        public Localizer T { get; set; }
        public string MenuName => "admin";

        public void GetNavigation(NavigationBuilder builder)
        {
            builder
                .Add(T("内容管理"), "66",
                    menu =>
                    {
                        menu.LinkToFirstChild(false);
                        menu.Add(T("文章内容"), "1", subitem => subitem
                            .Action("Index", "Admin", new { area = "ArticleManage" })
                            .Permission(ArticleManagePermissions.ViewContent));
                        menu.Add(T("文章栏目"), "0",
                            item => item.Action("Column", "Admin", new { area = "ArticleManage" })
                                .Permission(ArticleManagePermissions.ViewContent));
                    });
        }
    }
}