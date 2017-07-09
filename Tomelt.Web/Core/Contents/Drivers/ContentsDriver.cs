﻿using System.Collections.Generic;
using Tomelt.ContentManagement;
using Tomelt.ContentManagement.Drivers;
using Tomelt.Core.Contents.Settings;

namespace Tomelt.Core.Contents.Drivers {
    public class ContentsDriver : ContentPartDriver<ContentPart> {
        protected override DriverResult Display(ContentPart part, string displayType, dynamic shapeHelper) {
            return Combined(
                ContentShape("Parts_Contents_Publish",
                             () => shapeHelper.Parts_Contents_Publish()),
                ContentShape("Parts_Contents_Publish_Summary",
                             () => shapeHelper.Parts_Contents_Publish_Summary()),
                ContentShape("Parts_Contents_Publish_SummaryAdmin",
                             () => shapeHelper.Parts_Contents_Publish_SummaryAdmin()),
                ContentShape("Parts_Contents_Clone_SummaryAdmin",
                             () => shapeHelper.Parts_Contents_Clone_SummaryAdmin())
                );
        }

        protected override DriverResult Editor(ContentPart part, dynamic shapeHelper) {
            var results = new List<DriverResult> { ContentShape("Content_SaveButton", saveButton => saveButton) };

            if (part.TypeDefinition.Settings.GetModel<ContentTypeSettings>().Draftable)
                results.Add(ContentShape("Content_PublishButton", publishButton => publishButton));

            return Combined(results.ToArray());
        }

        protected override DriverResult Editor(ContentPart part, IUpdateModel updater, dynamic shapeHelper) {
            return Editor(part, updater);
        }
    }
}