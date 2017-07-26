using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArticleManage.ViewModels;
using Tomelt;
using Tomelt.ContentManagement;
using Tomelt.UI.Navigation;

namespace ArticleManage.Services
{
  public  interface IArticleService: IDependency
    {
        void UpdateForContentItem(ContentItem item, EditArticlePartViewModel model);
        IEnumerable<ContentItem> GetArticles(VersionOptions versionOptions);

        IEnumerable<ContentItem> GetArticlesPro(DatagridPagerParameters pagerParameters);
    }
}
