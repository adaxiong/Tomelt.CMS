using Tomelt.Events;
using Tomelt.Projections.Descriptors.Property;

namespace Tomelt.Projections.Services {
    public interface IPropertyProvider : IEventHandler {
        void Describe(DescribePropertyContext describe);
    }
}