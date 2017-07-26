using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ArticleManage.ViewModels
{
    public class EditColumnPartViewModel
    {
        [Display(Name = "上级栏目")]
        public int ParentId { get; set; }
        [Display(Name = "排序")]
        public int Sort { get; set; }
        [Display(Name = "栏目摘要")]
        public string Summary { get; set; }
        [Display(Name = "栏目索引")]
        public string CallIndex { get; set; }
        [Display(Name = "栏目链接")]
        public string LinkUrl { get; set; }
        [Display(Name = "封面图片")]
        public string ImageUrl { get; set; }
        [Display(Name = "树路径")]
        public string TreePath { get; set; }
        [Display(Name = "栏目级别")]
        public int Layer { get; set; }
        [Display(Name = "栏目组")]
        public string Groups { get; set; }
    }
}