using System;
using System.Web.Mvc;

namespace Tomelt.Mvc.AntiForgery {
    [AttributeUsage(AttributeTargets.Method)]
    public class ValidateAntiForgeryTokenTomeltAttribute : FilterAttribute {
        private readonly bool _enabled = true;

        public ValidateAntiForgeryTokenTomeltAttribute() : this(true) {}

        public ValidateAntiForgeryTokenTomeltAttribute(bool enabled) {
            _enabled = enabled;
        }

        public bool Enabled { get { return _enabled; } }
    }
}