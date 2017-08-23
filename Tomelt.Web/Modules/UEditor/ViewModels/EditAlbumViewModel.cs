using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UEditor.Models;

namespace UEditor.ViewModels
{
    public class EditAlbumViewModel
    {
        public EditAlbumViewModel()
        {
            AlbumPartRecords=new List<AlbumPartRecord>();
        }
        public string Albums { get; set; }
        public List<AlbumPartRecord> AlbumPartRecords { get; set; }
    }
}