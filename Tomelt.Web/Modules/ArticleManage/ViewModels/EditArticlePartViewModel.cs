using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ArticleManage.Models;

namespace ArticleManage.ViewModels
{
    public class EditArticlePartViewModel
    {
        [Display(Name = "文章标题")]
        [Required]
        public string Title { get; set; }
        [Display(Name = "副标题")]
        public string Subtitle { get; set; }
        [Display(Name = "内容摘要")]
        public string Summary { get; set; }
        [Display(Name = "文章外链")]
        public string LinkUrl { get; set; }
        [Display(Name = "文章栏目")]
        public string ColumnPartRecordTitle { get; set; }
        [Display(Name = "栏目主键")]
        public int ColumnPartRecordId { get; set; }
        [Display(Name = "排序")]
        public int Sort { get; set; }
        [Display(Name = "幻灯片")]
        public bool IsSlide { get; set; }
        [Display(Name = "置顶")]
        public bool IsTop { get; set; }
        [Display(Name = "热点")]
        public bool IsHot { get; set; }
        [Display(Name = "推荐")]
        public bool IsRecommend { get; set; }
        [Display(Name = "醒目")]
        public bool IsStriking { get; set; }
        [Display(Name = "来源")]
        public string Source { get; set; }
        [Display(Name = "作者")]
        public string Author { get; set; }
        [Display(Name = "人气值")]
        public int ClickNum { get; set; }
    }
}