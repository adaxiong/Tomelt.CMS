using System.Linq;
using System.Collections.Generic;
using Tomelt.Environment.Extensions;
using Tomelt.Localization;
using Tomelt.UI.Admin.Notification;
using Tomelt.UI.Notify;

namespace Tomelt.Core.Dashboard.Services {
    public class CompilationErrorBanner : INotificationProvider {
        private readonly ICriticalErrorProvider _errorProvider;

        public CompilationErrorBanner(ICriticalErrorProvider errorProvider) {
            _errorProvider = errorProvider;
            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        public IEnumerable<NotifyEntry> GetNotifications() {
            return _errorProvider.GetErrors()
                .Select(message => new NotifyEntry { Message = message, Type = NotifyType.Error });
        }
    }
}
