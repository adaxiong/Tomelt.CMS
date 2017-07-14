using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tomelt.EasyUIFields.Settings
{
    public class TextBoxFieldSettings
    {
        public string Width { get; set; }
        public int Height { get; set; }
        public bool Readonly { get; set; }
        public bool Editable { get; set; }
        public bool Disabled { get; set; }
        public bool Multiline { get; set; }
        public bool Required { get; set; }
        public string Prompt { get; set; }
        public string LabelAlign { get; set; }
        public string LabelPosition { get; set; }
        public string DefaultValue { get; set; }
    }
}