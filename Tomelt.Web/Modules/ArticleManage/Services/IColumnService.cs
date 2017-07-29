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
  public  interface IColumnService : IDependency
    {
        void UpdateForContentItem(ContentItem item, EditColumnPartViewModel viewModel);
        IContentQuery<ContentItem> GetColumns(VersionOptions versionOptions);
        List<EasyuiTree> GetTreeColumns(VersionOptions versionOptions);
    }
}
