using System;
using Tomelt.ContentManagement.Handlers;
using Tomelt.Core.Navigation.Models;
using Tomelt.Data;

namespace Tomelt.Core.Navigation.Handlers {
    public class AdminMenuPartHandler : ContentHandler {
        public AdminMenuPartHandler(IRepository<AdminMenuPartRecord> menuPartRepository) {
            Filters.Add(StorageFilter.For(menuPartRepository));

            OnInitializing<AdminMenuPart>((ctx, x) => {
                                      x.OnAdminMenu = false;
                                      x.AdminMenuText = String.Empty;
                                  });
        }
    }
}