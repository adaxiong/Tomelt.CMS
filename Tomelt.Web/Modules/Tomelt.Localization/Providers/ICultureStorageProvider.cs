namespace Tomelt.Localization.Providers {
    public interface ICultureStorageProvider : IDependency {
        void SetCulture(string culture);
    }
}