﻿using System.ComponentModel.DataAnnotations;
using Tomelt.ContentManagement.Records;
using Tomelt.Data.Conventions;

namespace ArticleManage.Models {
    public class ArticlePartRecord : ContentPartRecord {


        [Display(Name = "副标题")]
        public virtual string Subtitle { get; set; }
        [Display(Name = "内容摘要")]
        public virtual string Summary { get; set; }
        [Display(Name = "文章外链")]
        public virtual string LinkUrl { get; set; }
        [Display(Name = "文章栏目")]
        public virtual ColumnPartRecord ColumnPartRecord { get; set; }
        [Display(Name = "栏目主键")]
        public virtual int ColumnPartRecordId { get; set; }
        [Display(Name = "排序")]
        public virtual int Sort { get; set; }
        [Display(Name = "轮播")]
        public virtual bool IsSlide { get; set; }
        [Display(Name = "置顶")]
        public virtual bool IsTop { get; set; }
        [Display(Name = "热点")]
        public virtual bool IsHot { get; set; }
        [Display(Name = "推荐")]
        public virtual bool IsRecommend { get; set; }
        [Display(Name = "醒目")]
        public virtual bool IsStriking { get; set; }
        [Display(Name = "来源")]
        public virtual string Source { get; set; }
        [Display(Name = "作者")]
        public virtual string Author { get; set; }
        [Display(Name = "人气值")]
        public virtual int ClickNum { get; set; }


        //public static ColumnPartRecord DeserializeColumnPartRecord(string rawStateRecord)
        //{
        //    if (rawStateRecord == null)
        //    {
        //        return new ColumnPartRecord();
        //    }

        //    var stateRecordArray = rawStateRecord.Split(new[] { ',' });

        //    return new ColumnPartRecord()
        //    {
        //        Id = string.IsNullOrEmpty(stateRecordArray[0]) ? 0 : int.Parse(stateRecordArray[0]),
        //        Code = stateRecordArray[1],
        //        Name = stateRecordArray[2]
        //    };
        //}

        //public static string SerializeColumnPartRecord(ColumnPartRecord stateRecord)
        //{
        //    if (stateRecord == null)
        //    {
        //        return "";
        //    }

        //    return string.Join(",", stateRecord.Id, stateRecord.Code, stateRecord.Name);
        //}
    }
}