using System;

namespace Tomelt.Environment.Extensions {
    [AttributeUsage(AttributeTargets.Class)]
    public class TomeltFeatureAttribute : Attribute {
        public TomeltFeatureAttribute(string text) {
            FeatureName = text;
        }

        public string FeatureName { get; set; }
    }
}