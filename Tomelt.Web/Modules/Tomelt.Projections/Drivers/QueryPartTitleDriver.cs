﻿using System.Linq;
using Tomelt.ContentManagement;
using Tomelt.ContentManagement.Drivers;
using Tomelt.Core.Title.Models;
using Tomelt.Localization;

namespace Tomelt.Projections.Drivers {
    public class QueryPartTitleDriver : ContentPartDriver<TitlePart> {
        private readonly IContentManager _contentManager;

        public QueryPartTitleDriver(IContentManager contentManager) {
            _contentManager = contentManager;
        }

        public Localizer T { get; set; }

        protected override DriverResult Editor(TitlePart part, IUpdateModel updater, dynamic shapeHelper) {
            if (updater == null) {
                return null;
            }

            if(part.ContentItem.ContentType != "Query") {
                return null;
            }

            updater.TryUpdateModel(part, "Title", null, null);

            var query = _contentManager.Query("Query").Where<TitlePartRecord>(x => x.Title == part.Title).Slice(0, 1).FirstOrDefault();
            if (query != null && query.Id != part.ContentItem.Id) {
                updater.AddModelError("Title", T("A query with the same title already exists"));
            }

            return null;
        }
    }
}