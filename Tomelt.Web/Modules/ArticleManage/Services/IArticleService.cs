using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArticleManage.ViewModels;
using Tomelt;
using Tomelt.ContentManagement;

namespace ArticleManage.Services
{
  public  interface IArticleService: IDependency
    {
        void UpdateForContentItem(ContentItem item, EditArticlePartViewModel model);
        IEnumerable<ContentItem> GetArticles(VersionOptions versionOptions);
    }
}
