using System.Collections.Generic;

namespace Tomelt.Layouts.Models {
    public class SnippetDescriptor {
        public SnippetDescriptor() {
            Fields = new List<SnippetFieldDescriptor>();
        }

        public IList<SnippetFieldDescriptor> Fields { get; set; }
    }
}