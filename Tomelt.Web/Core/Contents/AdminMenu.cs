using System.Linq;
using Tomelt.ContentManagement;
using Tomelt.ContentManagement.MetaData;
using Tomelt.Core.Contents.Settings;
using Tomelt.Localization;
using Tomelt.Security;
using Tomelt.UI.Navigation;

namespace Tomelt.Core.Contents {
    public class AdminMenu : INavigationProvider {
        private readonly IContentDefinitionManager _contentDefinitionManager;
        private readonly IContentManager _contentManager;
        private readonly IAuthorizer _authorizer;

        public AdminMenu(IContentDefinitionManager contentDefinitionManager, IContentManager contentManager, IAuthorizer authorizer) {
            _contentDefinitionManager = contentDefinitionManager;
            _contentManager = contentManager;
            _authorizer = authorizer;
        }

        public Localizer T { get; set; }
        public string MenuName { get { return "admin"; } }

        //public void GetNavigation(NavigationBuilder builder) {
        //    var contentTypeDefinitions = _contentDefinitionManager.ListTypeDefinitions().OrderBy(d => d.Name);
        //    var listableContentTypes = contentTypeDefinitions.Where(ctd => ctd.Settings.GetModel<ContentTypeSettings>().Listable);
        //    ContentItem listableCi = null;
        //    foreach(var contentTypeDefinition in listableContentTypes) {
        //        listableCi = _contentManager.New(contentTypeDefinition.Name);
        //        if(_authorizer.Authorize(Permissions.EditContent, listableCi)) {
        //            builder.AddImageSet("content")
        //                .Add(T("Content"), "1.4", menu => menu
        //                .Add(T("Content Items"), "1", item => item.Action("List", "Admin", new { area = "Contents", id = "" }).LocalNav()));
        //            break;
        //        }
        //    }
        //    var contentTypes = contentTypeDefinitions.Where(ctd => ctd.Settings.GetModel<ContentTypeSettings>().Creatable).OrderBy(ctd => ctd.DisplayName);
        //    if (contentTypes.Any()) {
        //        builder.Add(T("New"), "-1", menu => {
        //            menu.LinkToFirstChild(false);
        //            foreach (var contentTypeDefinition in contentTypes) {
        //                var ci = _contentManager.New(contentTypeDefinition.Name);
        //                var cim = _contentManager.GetItemMetadata(ci);
        //                var createRouteValues = cim.CreateRouteValues;
        //                // review: the display name should be a LocalizedString
        //                if (createRouteValues.Any())
        //                    menu.Add(T(contentTypeDefinition.DisplayName), "5", item => item.Action(cim.CreateRouteValues["Action"] as string, cim.CreateRouteValues["Controller"] as string, cim.CreateRouteValues)
        //                        // Apply "PublishOwn" permission for the content type
        //                        .Permission(DynamicPermissions.CreateDynamicPermission(DynamicPermissions.PermissionTemplates[Permissions.PublishOwnContent.Name], contentTypeDefinition)));
        //            }
        //        });
        //    }
        //}

        public void GetNavigation(NavigationBuilder builder)
        {
            //builder.AddImageSet("ok")
            //    .Add(T("系统功能"), "88", menu =>
            //        {
            //            menu.LinkToFirstChild(false);
            //            menu.Add(T("内容管理"), "98",
            //                item => item.Action("List", "Admin", new {area = "Contents", id = ""}));
            //        }
            //    );
        }
    }
}