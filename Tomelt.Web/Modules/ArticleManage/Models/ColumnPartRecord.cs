using System.ComponentModel.DataAnnotations;
using Tomelt.ContentManagement.Records;

namespace ArticleManage.Models {
    public class ColumnPartRecord : ContentPartRecord
    {
        [Display(Name = "上级栏目")]
        public virtual int ParentId { get; set; }
        [Display(Name = "排序")]
        public virtual int Sort { get; set; }
        [Display(Name = "栏目摘要")]
        public virtual string Summary { get; set; }
        [Display(Name = "栏目索引")]
        public virtual string CallIndex { get; set; }
        [Display(Name = "栏目链接")]
        public virtual string LinkUrl { get; set; }
        [Display(Name = "封面图片")]
        public virtual string ImageUrl { get; set; }
        [Display(Name = "树路径")]
        public virtual string TreePath { get; set; }
        [Display(Name = "栏目级别")]
        public virtual int Layer { get; set; }
        [Display(Name = "栏目组")]
        public virtual string Groups { get; set; }
    }
}