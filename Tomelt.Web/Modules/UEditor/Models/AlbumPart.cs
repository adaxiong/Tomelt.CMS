using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tomelt.ContentManagement;

namespace UEditor.Models
{
    public class AlbumPart: ContentPart<AlbumPartRecord>
    {
        public IEnumerable<string> CurrentAlbumRecords
        {
            get { return ParseAlbumRecords(Retrieve<string>("CurrentAlbumRecords")); }
            set { Store("CurrentAlbumRecords", String.Join(",", value)); }
        }

        private IEnumerable<string> ParseAlbumRecords(string albumRecords)
        {
            return (albumRecords ?? "").Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}