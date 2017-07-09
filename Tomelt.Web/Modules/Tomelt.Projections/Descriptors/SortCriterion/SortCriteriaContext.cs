using System.Collections.Generic;
using Tomelt.ContentManagement;

namespace Tomelt.Projections.Descriptors.SortCriterion {
    public class SortCriterionContext {
        public SortCriterionContext() {
            Tokens = new Dictionary<string, object>();
        }

        public IDictionary<string, object> Tokens { get; set; }
        public dynamic State { get; set; }
        public IHqlQuery Query { get; set; }
    }
}