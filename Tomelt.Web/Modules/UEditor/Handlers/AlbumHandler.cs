using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tomelt.ContentManagement.Handlers;
using Tomelt.Data;
using UEditor.Models;

namespace UEditor.Handlers
{
    public class AlbumHandler: ContentHandler
    {
        public AlbumHandler(IRepository<AlbumPartRecord> repository)
        {
            Filters.Add(StorageFilter.For(repository));
        }
    }
}