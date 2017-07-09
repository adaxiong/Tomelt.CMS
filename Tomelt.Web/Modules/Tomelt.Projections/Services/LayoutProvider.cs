using Tomelt.Events;
using Tomelt.Projections.Descriptors.Layout;

namespace Tomelt.Projections.Services {
    public interface ILayoutProvider : IEventHandler {
        void Describe(DescribeLayoutContext describe);
    }
}