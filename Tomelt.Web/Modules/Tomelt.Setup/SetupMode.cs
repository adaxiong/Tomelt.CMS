using System;
using System.Web.Routing;
using Autofac;
using Tomelt.Caching;
using Tomelt.Commands;
using Tomelt.Commands.Builtin;
using Tomelt.ContentManagement;
using Tomelt.ContentManagement.Handlers;
using Tomelt.ContentManagement.MetaData.Builders;
using Tomelt.Core.Settings.Models;
using Tomelt.Data.Migration.Interpreters;
using Tomelt.Data.Providers;
using Tomelt.Data.Migration;
using Tomelt.DisplayManagement;
using Tomelt.DisplayManagement.Descriptors;
using Tomelt.DisplayManagement.Descriptors.ShapeAttributeStrategy;
using Tomelt.DisplayManagement.Descriptors.ShapeTemplateStrategy;
using Tomelt.DisplayManagement.Implementation;
using Tomelt.Environment;
using Tomelt.Environment.Extensions.Models;
using Tomelt.Localization;
using Tomelt.Mvc;
using Tomelt.Mvc.ModelBinders;
using Tomelt.Mvc.Routes;
using Tomelt.Mvc.ViewEngines;
using Tomelt.Mvc.ViewEngines.Razor;
using Tomelt.Mvc.ViewEngines.ThemeAwareness;
using Tomelt.Recipes.Services;
using Tomelt.Settings;
using Tomelt.Tasks;
using Tomelt.Tasks.Locking;
using Tomelt.Themes;
using Tomelt.UI.Notify;
using Tomelt.UI.PageClass;
using Tomelt.UI.PageTitle;
using Tomelt.UI.Resources;
using Tomelt.UI.Zones;
using IFilterProvider = Tomelt.Mvc.Filters.IFilterProvider;

namespace Tomelt.Setup {
    public class SetupMode : Module {
        public Feature Feature { get; set; }

        protected override void Load(ContainerBuilder builder) {

            // standard services needed in setup mode
            builder.RegisterModule(new MvcModule());
            builder.RegisterModule(new CommandModule());
            builder.RegisterModule(new WorkContextModule());
            builder.RegisterModule(new CacheModule());

            builder.RegisterType<RoutePublisher>().As<IRoutePublisher>().InstancePerLifetimeScope();
            builder.RegisterType<ModelBinderPublisher>().As<IModelBinderPublisher>().InstancePerLifetimeScope();
            builder.RegisterType<RazorViewEngineProvider>().As<IViewEngineProvider>().As<IShapeTemplateViewEngine>().SingleInstance();
            builder.RegisterType<ThemedViewResultFilter>().As<IFilterProvider>().InstancePerLifetimeScope();
            builder.RegisterType<ThemeFilter>().As<IFilterProvider>().InstancePerLifetimeScope();
            builder.RegisterType<PageTitleBuilder>().As<IPageTitleBuilder>().InstancePerLifetimeScope();
            builder.RegisterType<PageClassBuilder>().As<IPageClassBuilder>().InstancePerLifetimeScope();
            builder.RegisterType<Notifier>().As<INotifier>().InstancePerLifetimeScope();
            builder.RegisterType<NotifyFilter>().As<IFilterProvider>().InstancePerLifetimeScope();
            builder.RegisterType<DataServicesProviderFactory>().As<IDataServicesProviderFactory>().InstancePerLifetimeScope();
            builder.RegisterType<DefaultCommandManager>().As<ICommandManager>().InstancePerLifetimeScope();
            builder.RegisterType<HelpCommand>().As<ICommandHandler>().InstancePerLifetimeScope();
            builder.RegisterType<ResourceManager>().As<IResourceManager>().InstancePerLifetimeScope();
            builder.RegisterType<ResourceFilter>().As<IFilterProvider>().InstancePerLifetimeScope();
            builder.RegisterType<DefaultTomeltShell>().As<ITomeltShell>().InstancePerMatchingLifetimeScope("shell");
            builder.RegisterType<SweepGenerator>().As<ISweepGenerator>().SingleInstance();
            builder.RegisterType<SetupBackgroundService>().As<IBackgroundService>().InstancePerLifetimeScope();

            // setup mode specific implementations of needed service interfaces
            builder.RegisterType<SafeModeThemeService>().As<IThemeManager>().InstancePerLifetimeScope();
            builder.RegisterType<SafeModeText>().As<IText>().InstancePerLifetimeScope();
            builder.RegisterType<SafeModeSiteService>().As<ISiteService>().InstancePerLifetimeScope();

            builder.RegisterType<DefaultDataMigrationInterpreter>().As<IDataMigrationInterpreter>().InstancePerLifetimeScope();
            builder.RegisterType<DataMigrationManager>().As<IDataMigrationManager>().InstancePerLifetimeScope();

            builder.RegisterType<RecipeHarvester>().As<IRecipeHarvester>().InstancePerLifetimeScope();
            builder.RegisterType<RecipeParser>().As<IRecipeParser>().InstancePerLifetimeScope();

            builder.RegisterType<DefaultCacheHolder>().As<ICacheHolder>().SingleInstance();

            // in progress - adding services for display/shape support in setup
            builder.RegisterType<DisplayHelperFactory>().As<IDisplayHelperFactory>().InstancePerLifetimeScope();
            builder.RegisterType<DefaultDisplayManager>().As<IDisplayManager>().InstancePerLifetimeScope();
            builder.RegisterType<DefaultShapeFactory>().As<IShapeFactory>().InstancePerLifetimeScope();
            builder.RegisterType<DefaultShapeTableManager>().As<IShapeTableManager>().InstancePerLifetimeScope();
            builder.RegisterType<ShapeTableLocator>().As<IShapeTableLocator>().InstancePerMatchingLifetimeScope("work");

            builder.RegisterType<ThemeAwareViewEngine>().As<IThemeAwareViewEngine>().InstancePerLifetimeScope();
            builder.RegisterType<LayoutAwareViewEngine>().As<ILayoutAwareViewEngine>().InstancePerLifetimeScope();
            builder.RegisterType<ConfiguredEnginesCache>().As<IConfiguredEnginesCache>().SingleInstance();
            builder.RegisterType<LayoutWorkContext>().As<IWorkContextStateProvider>().InstancePerLifetimeScope();
            builder.RegisterType<SafeModeSiteWorkContextProvider>().As<IWorkContextStateProvider>().InstancePerLifetimeScope();

            builder.RegisterType<ShapeTemplateBindingStrategy>().As<IShapeTableProvider>().InstancePerLifetimeScope();
            builder.RegisterType<BasicShapeTemplateHarvester>().As<IShapeTemplateHarvester>().InstancePerLifetimeScope();
            builder.RegisterType<ShapeAttributeBindingStrategy>().As<IShapeTableProvider>().InstancePerMatchingLifetimeScope("shell");
            builder.RegisterModule(new ShapeAttributeBindingModule());
        }


        internal class SetupBackgroundService : IBackgroundService {
            public void Sweep() {
                // Don't run any background service in setup mode.
            }
        }

        class SafeModeText : IText {
            public LocalizedString Get(string textHint, params object[] args) {
                if (args == null || args.Length == 0) {
                    return new LocalizedString(textHint);
                }
                return new LocalizedString(string.Format(textHint, args));
            }
        }

        class SafeModeThemeService : IThemeManager {
            private readonly ExtensionDescriptor _theme = new ExtensionDescriptor {
                Id = "SafeMode",
                Name = "SafeMode",
                Location = "~/Themes",
            };

            public ExtensionDescriptor GetRequestTheme(RequestContext requestContext) { return _theme; }
        }

        class SafeModeSiteWorkContextProvider : IWorkContextStateProvider {
            public Func<WorkContext, T> Get<T>(string name) {
                if (name == "CurrentSite") {
                    ISite safeModeSite = new SafeModeSite();
                    return ctx => (T)safeModeSite;
                }
                return null;
            }
        }

        class SafeModeSiteService : ISiteService {
            public ISite GetSiteSettings() {
                var siteType = new ContentTypeDefinitionBuilder().Named("Site").Build();
                var site = new ContentItemBuilder(siteType)
                    .Weld<SafeModeSite>()
                    .Build();

                return site.As<ISite>();
            }
        }

        class SafeModeSite : ContentPart, ISite {
            public string PageTitleSeparator {
                get { return " - "; }
            }

            public string SiteName {
                get { return "Tomelt CMS 系统"; }
            }

            public string SiteSalt {
                get { return "42"; }
            }

            public string SiteUrl {
                get { return "/"; }
            }

            public string SuperUser {
                get { return ""; }
            }

            public string HomePage {
                get { return ""; }
                set { throw new NotImplementedException(); }
            }

            public string SiteCulture {
                get { return ""; }
                set { throw new NotImplementedException(); }
            }

            public string SiteCalendar {
                get { return ""; }
                set { throw new NotImplementedException(); }
            }

            public ResourceDebugMode ResourceDebugMode {
                get { return ResourceDebugMode.FromAppSetting; }
                set { throw new NotImplementedException(); }
            }

            public bool UseCdn {
                get { return false; }
                set { throw new NotImplementedException(); }
            }

            public int PageSize {
                get { return SiteSettingsPart.DefaultPageSize; }
                set { throw new NotImplementedException(); }
            }

            public int MaxPageSize {
                get { return SiteSettingsPart.DefaultPageSize; }
                set { throw new NotImplementedException(); }
            }

            public int MaxPagedCount {
                get { return 0; }
                set { throw new NotImplementedException(); }
            }

            public string BaseUrl {
                get { return ""; }
            }

            public string SiteTimeZone {
                get { return TimeZoneInfo.Local.Id; }
             }        
        }
    }
}
