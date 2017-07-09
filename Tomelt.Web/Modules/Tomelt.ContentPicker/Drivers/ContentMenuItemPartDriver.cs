using Tomelt.ContentManagement;
using Tomelt.ContentManagement.Drivers;
using Tomelt.ContentManagement.Handlers;
using Tomelt.ContentPicker.Models;
using Tomelt.ContentPicker.ViewModels;
using Tomelt.Core.Navigation;
using Tomelt.Core.Navigation.Models;
using Tomelt.Core.Navigation.ViewModels;
using Tomelt.Localization;
using Tomelt.Security;

namespace Tomelt.ContentPicker.Drivers {
    public class ContentMenuItemPartDriver : ContentPartDriver<ContentMenuItemPart> {
        private readonly IContentManager _contentManager;
        private readonly IAuthorizationService _authorizationService;
        private readonly IWorkContextAccessor _workContextAccessor;

        public ContentMenuItemPartDriver(
            IContentManager contentManager,
            IAuthorizationService authorizationService, 
            IWorkContextAccessor workContextAccessor) {
            _contentManager = contentManager;
            _authorizationService = authorizationService;
            _workContextAccessor = workContextAccessor;

            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        protected override DriverResult Editor(ContentMenuItemPart part, dynamic shapeHelper) {
            return ContentShape("Parts_ContentMenuItem_Edit",
                                () => {
                                    var model = new ContentMenuItemEditViewModel {
                                        ContentItemId = part.Content == null ? -1 : part.Content.Id,
                                        Part = part
                                    };
                                    return shapeHelper.EditorTemplate(TemplateName: "Parts.ContentMenuItem.Edit", Model: model, Prefix: Prefix);
                                });
        }

        protected override DriverResult Editor(ContentMenuItemPart part, IUpdateModel updater, dynamic shapeHelper) {
            var currentUser = _workContextAccessor.GetContext().CurrentUser;
            if (!_authorizationService.TryCheckAccess(Permissions.ManageMenus, currentUser, part))
                return null;

            var model = new ContentMenuItemEditViewModel();

            if(updater.TryUpdateModel(model, Prefix, null, null)) {
                var contentItem = _contentManager.Get(model.ContentItemId, VersionOptions.Latest);
                if(contentItem == null) {
                    updater.AddModelError("ContentItemId", T("You must select a Content Item"));
                }
                else {
                    part.Content = contentItem;
                }
            }

            return Editor(part, shapeHelper);
        }

        protected override void Importing(ContentMenuItemPart part, ImportContentContext context) {
            // Don't do anything if the tag is not specified.
            if (context.Data.Element(part.PartDefinition.Name) == null) {
                return;
            }

            context.ImportAttribute(part.PartDefinition.Name, "ContentItem", 
                contentItemId => {
                    var contentItem = context.GetItemFromSession(contentItemId);
                    part.Content = contentItem;
                }, () => 
                    part.Content = null
            );
        }

        protected override void Exporting(ContentMenuItemPart part, ExportContentContext context) {
            if (part.Content != null) {
                var contentItem = _contentManager.Get(part.Content.Id);
                if (contentItem != null) {
                    var containerIdentity = _contentManager.GetItemMetadata(contentItem).Identity;
                    context.Element(part.PartDefinition.Name).SetAttributeValue("ContentItem", containerIdentity.ToString());
                }
            }
        }
    }
}