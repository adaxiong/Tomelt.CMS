using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UEditor.Models
{
    public class AlbumRecord
    {
        public virtual int Id { get; set; }
        public virtual string ThumbPath { get; set; }
        public virtual string OriginalPath { get; set; }
        public virtual string Describe { get; set; }
    }
}