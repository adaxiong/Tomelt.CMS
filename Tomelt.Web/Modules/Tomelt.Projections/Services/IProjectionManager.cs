using System.Collections.Generic;
using Tomelt.ContentManagement;
using Tomelt.Projections.Descriptors;
using Tomelt.Projections.Descriptors.Property;
using Tomelt.Projections.Descriptors.Filter;
using Tomelt.Projections.Descriptors.Layout;
using Tomelt.Projections.Descriptors.SortCriterion;

namespace Tomelt.Projections.Services {
    public interface IProjectionManager : IDependency {
        IEnumerable<TypeDescriptor<FilterDescriptor>> DescribeFilters();
        IEnumerable<TypeDescriptor<SortCriterionDescriptor>> DescribeSortCriteria();
        IEnumerable<TypeDescriptor<LayoutDescriptor>> DescribeLayouts();
        IEnumerable<TypeDescriptor<PropertyDescriptor>> DescribeProperties();

        FilterDescriptor GetFilter(string category, string type);
        SortCriterionDescriptor GetSortCriterion(string category, string type);
        LayoutDescriptor GetLayout(string category, string type);
        PropertyDescriptor GetProperty(string category, string type);

        IEnumerable<ContentItem> GetContentItems(int queryId, int skip = 0, int count = 0);
        int GetCount(int queryId);
    }

}