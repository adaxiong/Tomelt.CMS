using Tomelt.Events;
using Tomelt.Projections.Descriptors.SortCriterion;

namespace Tomelt.Projections.Services {
    public interface ISortCriterionProvider : IEventHandler {
        void Describe(DescribeSortCriterionContext describe);
    }
}