using System.Collections.Generic;
using Tomelt.ContentManagement;
using Tomelt.Projections.Descriptors;
using Tomelt.Projections.Descriptors.Property;
using Tomelt.Projections.Descriptors.Filter;
using Tomelt.Projections.Descriptors.Layout;
using Tomelt.Projections.Descriptors.SortCriterion;

namespace Tomelt.Projections.Services {
    public interface IProjectionManagerExtension : IProjectionManager {

        IEnumerable<ContentItem> GetContentItems(int queryId, ContentPart part, int skip = 0, int count = 0);
        int GetCount(int queryId, ContentPart part);
    }

}