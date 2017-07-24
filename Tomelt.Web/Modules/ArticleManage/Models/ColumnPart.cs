using System.ComponentModel.DataAnnotations;
using Tomelt.ContentManagement;

namespace ArticleManage.Models {
    public class ColumnPart : ContentPart<ColumnPartRecord>
    {
        [Display(Name = "栏目标题")]
        public string Title
        {
            get { return Record.Title; }
            set { Record.Title = value; }
        }
        [Display(Name = "栏目摘要")]
        public string Summary
        {
            get { return Record.Summary; }
            set { Record.Summary = value; }
        }
        [Display(Name = "栏目索引")]
        public string CallIndex
        {
            get { return Record.CallIndex; }
            set { Record.CallIndex = value; }
        }
        [Display(Name = "封面图片")]
        public string ImageUrl
        {
            get { return Record.ImageUrl; }
            set { Record.ImageUrl = value; }
        }
        [Display(Name = "栏目链接")]
        public string LinkUrl
        {
            get { return Record.LinkUrl; }
            set { Record.LinkUrl = value; }
        }
        [Display(Name = "上级栏目")]
        public int ParentId
        {
            get { return Record.ParentId; }
            set { Record.ParentId = value; }
        }
        [Display(Name = "排序")]
        public int Sort
        {
            get { return Record.Sort; }
            set { Record.Sort = value; }
        }
        [Display(Name = "栏目级别")]
        public int Layer
        {
            get { return Record.Layer; }
            set { Record.Layer = value; }
        }
        [Display(Name = "树路径")]
        public string TreePath
        {
            get { return Record.TreePath; }
            set { Record.TreePath = value; }
        }
        [Display(Name = "栏目组")]
        public string Groups
        {
            get { return Record.Groups; }
            set { Record.Groups = value; }
        }
    }
}