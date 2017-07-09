﻿using Tomelt.ContentManagement;
using Tomelt.ContentPicker.Models;
using Tomelt.Security;

namespace Tomelt.ContentPicker.Security {
    public class ContentMenuItemAuthorizationEventHandler : IAuthorizationServiceEventHandler{
        private readonly IAuthorizationService _authorizationService;

        public ContentMenuItemAuthorizationEventHandler(IAuthorizationService authorizationService) {
            _authorizationService = authorizationService;
        }

        public void Checking(CheckAccessContext context) { }
        public void Adjust(CheckAccessContext context) { }

        public void Complete(CheckAccessContext context) {
            if (context.Content == null) {
                return;
            }

            var part = context.Content.As<ContentMenuItemPart>();

            // if the content item has no right attached, check on the container
            if (part == null) {
                return;
            }

            context.Granted = _authorizationService.TryCheckAccess(context.Permission, context.User, part.Content);
            context.Adjusted = true;
        }
    }
}