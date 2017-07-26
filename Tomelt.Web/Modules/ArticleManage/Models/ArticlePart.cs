using System.ComponentModel.DataAnnotations;
using Tomelt.ContentManagement;

namespace ArticleManage.Models {
    public class ArticlePart : ContentPart<ArticlePartRecord>
    {
        [Display(Name = "内容摘要")]
        public string Summary
        {
            get { return Record.Summary; }
            set { Record.Summary = value; }
        }
        [Display(Name = "文章栏目")]
        public ColumnPartRecord ColumnPartRecord
        {
            get { return Record.ColumnPartRecord; }
            set { Record.ColumnPartRecord = value; }
        }
        [Display(Name = "副标题")]
        public string Subtitle
        {
            get { return Record.Subtitle; }
            set { Record.Subtitle = value; }
        }
        [Display(Name = "文章外链")]
        public string LinkUrl
        {
            get { return Record.LinkUrl; }
            set { Record.LinkUrl = value; }
        }

        [Display(Name = "排序")]
        public int Sort
        {
            get { return Record.Sort; }
            set { Record.Sort = value; }
        }
        [Display(Name = "幻灯片")]
        public bool IsSlide
        {
            get { return Record.IsSlide; }
            set { Record.IsSlide = value; }
        }
        [Display(Name = "热点")]
        public bool IsHot
        {
            get { return Record.IsHot; }
            set { Record.IsHot = value; }
        }
        [Display(Name = "置顶")]
        public bool IsTop
        {
            get { return Record.IsTop; }
            set { Record.IsTop = value; }
        }
        [Display(Name = "推荐")]
        public bool IsRecommend
        {
            get { return Record.IsRecommend; }
            set { Record.IsRecommend = value; }
        }
        [Display(Name = "醒目")]
        public bool IsStriking
        {
            get { return Record.IsStriking; }
            set { Record.IsStriking = value; }
        }
        [Display(Name = "来源")]
        public string Source
        {
            get { return Record.Source; }
            set { Record.Source = value; }
        }
        [Display(Name = "作者")]
        public string Author
        {
            get { return Record.Author; }
            set { Record.Author = value; }
        }
        [Display(Name = "人气值")]
        public int ClickNum
        {
            get { return Record.ClickNum; }
            set { Record.ClickNum = value; }
        }
        [Display(Name = "栏目主键")]
        public int ColumnPartRecordId
        {
            get { return Record.ColumnPartRecordId; }
            set { Record.ColumnPartRecordId = value; }
        }
    }
}