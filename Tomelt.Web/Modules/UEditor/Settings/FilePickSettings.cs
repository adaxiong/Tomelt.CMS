using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UEditor.Settings
{
    public class FilePickSettings
    {
        public string Width { get; set; }
        public int Height { get; set; }
        public bool Editable { get; set; }
        public bool Required { get; set; }
        public string Prompt { get; set; }
        public string LabelAlign { get; set; }
        public string LabelPosition { get; set; }
    }
}