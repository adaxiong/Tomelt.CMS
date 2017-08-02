namespace Tomelt.UI.Navigation {
    public class PagerParameters {
        /// <summary>
        /// Gets or sets the current page number or null if none specified.
        /// </summary>
        public int? Page { get; set; }

        /// <summary>
        /// Gets or sets the current page size or null if none specified.
        /// </summary>
        public int? PageSize { get; set; }
    }
    public class DatagridPagerParameters
    {
        /// <summary>
        /// 当前页
        /// </summary>
        public int? page { get; set; }
        /// <summary>
        /// 当前页总条数
        /// </summary>
        public int? rows { get; set; }
        /// <summary>
        /// 总条数
        /// </summary>
        public int? total { get; set; }
        /// <summary>
        /// 升降序
        /// </summary>
        public string order { get; set; }
        ///// <summary>
        ///// 排序字段
        ///// </summary>
        //public string sort { get; set; }
        /// <summary>
        /// 内容状态
        /// </summary>
        public string contentStatus { get; set; }
    }
}
