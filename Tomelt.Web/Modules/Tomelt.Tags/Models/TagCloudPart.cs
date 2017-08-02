using System.Collections.Generic;
using Tomelt.ContentManagement;
using Tomelt.ContentManagement.Utilities;
using Tomelt.Environment.Extensions;

namespace Tomelt.Tags.Models {
    [TomeltFeature("Tomelt.Tags.TagCloud")]
    public class TagCloudPart : ContentPart {
        internal readonly LazyField<IList<TagCount>> _tagCountField = new LazyField<IList<TagCount>>();

        public IList<TagCount> TagCounts { get { return _tagCountField.Value; } }

        public int Buckets { 
            get { return this.Retrieve(r => r.Buckets, 5); }
            set { this.Store(r => r.Buckets, value); }
        }

        public string Slug {
            get { return this.Retrieve(r => r.Slug); }
            set { this.Store(r => r.Slug, value); }
        }
    }
}