using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UEditor.Models
{
    /// <summary>
    /// 相册内容记录
    /// </summary>
    public class ContentAlbumRecord
    {
        public virtual int Id { get; set; }
        public virtual string ThumbPath { get; set; }
        public virtual string OriginalPath { get; set; }
        public virtual string Describe { get; set; }
        public virtual AlbumPartRecord AlbumPartRecord { get; set; }
    }
}