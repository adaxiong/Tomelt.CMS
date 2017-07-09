namespace Tomelt.UI.Navigation {
    public interface INavigationProvider : IDependency {
        string MenuName { get; }
        void GetNavigation(NavigationBuilder builder);
    }
}