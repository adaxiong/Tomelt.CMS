using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using Autofac;
using Autofac.Core;

namespace Tomelt.Wcf {
    public class TomeltInstanceProvider : IInstanceProvider {
        private readonly IWorkContextAccessor _workContextAccessor;
        private readonly IComponentRegistration _componentRegistration;

        public TomeltInstanceProvider(IWorkContextAccessor workContextAccessor, IComponentRegistration componentRegistration) {
            _workContextAccessor = workContextAccessor;
            _componentRegistration = componentRegistration;
        }

        public object GetInstance(InstanceContext instanceContext, Message message) {
            TomeltInstanceContext item = new TomeltInstanceContext(_workContextAccessor);
            instanceContext.Extensions.Add(item);
            return item.Resolve(_componentRegistration);

        }

        public object GetInstance(InstanceContext instanceContext) {
            return GetInstance(instanceContext, null);
        }

        public void ReleaseInstance(InstanceContext instanceContext, object instance) {
            TomeltInstanceContext context = instanceContext.Extensions.Find<TomeltInstanceContext>();
            if (context != null) {
                context.Dispose();
            }
        }
    }
}
