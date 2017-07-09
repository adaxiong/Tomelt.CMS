using System.Collections.Generic;
using Tomelt.ContentManagement;
using Tomelt.Core.Title.Models;

namespace Tomelt.Projections.Models {
    public class QueryPart : ContentPart<QueryPartRecord> {

        public string Name {
            get { return this.As<TitlePart>().Title;  }
            set { this.As<TitlePart>().Title = value; }
        }

        public IList<SortCriterionRecord> SortCriteria {
            get { return Record.SortCriteria; }
        }

        public IList<FilterGroupRecord> FilterGroups {
            get { return Record.FilterGroups; }
        }

        public IList<LayoutRecord> Layouts {
            get { return Record.Layouts; }
        }
    }
}