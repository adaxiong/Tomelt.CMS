using System;

namespace Tomelt.Environment.Extensions {
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class TomeltSuppressDependencyAttribute : Attribute {
        public TomeltSuppressDependencyAttribute(string fullName) {
            FullName = fullName;
        }

        public string FullName { get; set; }
    }
}