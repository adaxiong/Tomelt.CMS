using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ArticleManage.Models;
using ArticleManage.ViewModels;
using Tomelt;
using Tomelt.ContentManagement;
using Tomelt.ContentManagement.Records;
using Tomelt.UI.Navigation;

namespace ArticleManage.Services
{
  public  interface IArticleService: IDependency
    {
        void UpdateForContentItem(ContentItem item);

        IContentQuery<ContentItem> GetArticles(VersionOptions versionOptions);

        IEnumerable<ContentItem> GetArticlesPro(EditArticlePartViewModel search);
        int GetArticlesCountByColumnId(int columnId);
    }
}
