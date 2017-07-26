using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ArticleManage.Models;
using Tomelt.ContentManagement;
using Tomelt.ContentManagement.Handlers;
using Tomelt.Core.Common.Models;
using Tomelt.Data;

namespace ArticleManage.Handlers
{
    public class ArticlePartHandler: ContentHandler
    {
        public ArticlePartHandler(IRepository<ArticlePartRecord> repository)
        {
            Filters.Add(StorageFilter.For(repository));
            //索引
            OnIndexing<ArticlePart>((context, part) => context.DocumentIndex
                .Add("标题", part.Record.Title).Analyze().Store()
                .Add("摘要", part.Record.Summary).Analyze().Store()
                .Add("内容", part.As<BodyPart>().Text).Analyze().Store()
                .Add("作者", part.Record.Author).Analyze().Store()
                .Add("来源", part.Record.Source).Analyze().Store()
            );
        }
    }
}