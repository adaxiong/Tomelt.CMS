using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tomelt.ContentManagement.Records;

namespace UEditor.Models
{
    public class AlbumPartRecord:ContentPartRecord
    {
        public AlbumPartRecord()
        {
            ContentAlbumRecords = new List<ContentAlbumRecord>();
        }

        public virtual IList<ContentAlbumRecord> ContentAlbumRecords { get; set; }
    }
}