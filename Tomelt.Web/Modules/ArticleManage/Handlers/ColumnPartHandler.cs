using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ArticleManage.Models;
using Tomelt.ContentManagement;
using Tomelt.ContentManagement.Handlers;
using Tomelt.Core.Title.Models;
using Tomelt.Data;

namespace ArticleManage.Handlers
{
    public class ColumnPartHandler: ContentHandler
    {
        public ColumnPartHandler(IRepository<ColumnPartRecord> repository)
        {
            Filters.Add(StorageFilter.For(repository));
            //索引
            OnIndexing<ColumnPart>((context, part) => context.DocumentIndex
                .Add("栏目ID", part.Record.Id).Analyze().Store()
                //.Add("栏目标题", part.As<TitlePart>().Title).Analyze().Store()

            );
        }
    }
}