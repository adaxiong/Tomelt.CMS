using System;
using System.Collections.Generic;
using System.Linq;

namespace Tomelt.Commands {
    [AttributeUsage(AttributeTargets.Method)]
    public class TomeltSwitchesAttribute : Attribute {
        private readonly string _switches;

        public TomeltSwitchesAttribute(string switches) {
            _switches = switches;
        }

        public IEnumerable<string> Switches {
            get {
                return (_switches ?? "").Trim().Split(',').Select(s => s.Trim());
            }
        }
    }
}
