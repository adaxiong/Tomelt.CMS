using Autofac;

namespace Tomelt.ContentManagement {
    public class ContentModule : Module {
        protected override void Load(ContainerBuilder builder) {
            builder.RegisterType<DefaultContentQuery>().As<IContentQuery>().InstancePerDependency();
        }
    }
}
