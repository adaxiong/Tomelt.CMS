using Tomelt.Events;
using Tomelt.Projections.Descriptors.Filter;

namespace Tomelt.Projections.Services {
    public interface IFilterProvider : IEventHandler {
        void Describe(DescribeFilterContext describe);
    }
}