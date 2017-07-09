using Tomelt.ContentManagement;
using Tomelt.ContentManagement.Handlers;

namespace Tomelt.Layouts.Framework.Drivers {
    public class ImportContentContextWrapper : IContentImportSession {
        private readonly ImportContentContext _context;

        public ImportContentContextWrapper(ImportContentContext context) {
            _context = context;
        }

        public ContentItem GetItemFromSession(string id) {
            return _context.GetItemFromSession(id);
        }
    }
}