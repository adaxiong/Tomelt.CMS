using Tomelt.Forms.Services;

namespace Tomelt.Layouts.Services {
    public interface IFormsBasedElementServices : IDependency
    {
        IFormManager FormManager { get; }
        ICultureAccessor CultureAccessor { get; }
    }
}