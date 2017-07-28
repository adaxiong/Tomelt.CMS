using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArticleManage.ViewModels
{
    public class EasyuiTree
    {
        /// <summary> 
        /// ID
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        public string text { get; set; }
        /// <summary>
        /// 是否折叠closed/open
        /// </summary>
        public string state { get; set; }
        /// 子类
        /// </summary>
        public List<EasyuiTree> children { get; set; }
        /// <summary>
        /// 是否选中
        /// </summary>
        public bool @checked { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string iconCls { get; set; }
        /// <summary>
        /// 父ID
        /// </summary>
        public int ParentId { get; set; }
    }
}