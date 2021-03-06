﻿using Tomelt.ContentManagement;

namespace Tomelt.UI.Navigation {
    public interface IMenuProvider : IDependency {
        void GetMenu(IContent menu, NavigationBuilder builder);
    }
}