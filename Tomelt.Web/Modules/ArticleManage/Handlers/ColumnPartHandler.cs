using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ArticleManage.Models;
using Tomelt.ContentManagement.Handlers;
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
                .Add("栏目标题", part.Record.Title).Analyze().Store()

            );
        }
    }
}