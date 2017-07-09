using Tomelt.Forms.Services;

namespace Tomelt.Layouts.Services {
    public class FormsBasedElementServices : IFormsBasedElementServices {
        public FormsBasedElementServices(IFormManager formManager, ICultureAccessor cultureAccessor) {
            FormManager = formManager;
            CultureAccessor = cultureAccessor;
        }

        public IFormManager FormManager { get; private set; }
        public ICultureAccessor CultureAccessor { get; private set; }
    }
}