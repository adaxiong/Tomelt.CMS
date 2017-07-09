using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;
using Tomelt.Environment;
using Tomelt.Events;
using Tomelt.Localization;
using Tomelt.Logging;
using Tomelt.Security;
using Tomelt.UI.Notify;

namespace Tomelt.Exceptions {
    public class DefaultExceptionPolicy : IExceptionPolicy {
        private readonly INotifier _notifier;
        private readonly Work<IAuthorizer> _authorizer;

        public DefaultExceptionPolicy() {
            Logger = NullLogger.Instance;
            T = NullLocalizer.Instance;
        }

        public DefaultExceptionPolicy(INotifier notifier, Work<IAuthorizer> authorizer) {
            _notifier = notifier;
            _authorizer = authorizer;
            Logger = NullLogger.Instance;
            T = NullLocalizer.Instance;
        }

        public ILogger Logger { get; set; }
        public Localizer T { get; set; }

        public bool HandleException(object sender, Exception exception) {
            if (IsFatal(exception)) {
                return false;
            }

            if (sender is IEventBus &&  exception is TomeltFatalException) {
                return false;
            }

            Logger.Error(exception, "An unexpected exception was caught");

            do {
                RaiseNotification(exception);
                exception = exception.InnerException;
            } while (exception != null);

            return true;
        }

        private static bool IsFatal(Exception exception) {
            return 
                exception is TomeltSecurityException ||
                exception is StackOverflowException ||
                exception is AccessViolationException ||
                exception is AppDomainUnloadedException ||
                exception is ThreadAbortException ||
                exception is SecurityException ||
                exception is SEHException;
        }

        private void RaiseNotification(Exception exception) {
            if (_notifier == null || _authorizer.Value == null) {
                return;
            }
            if (exception is TomeltException) {
                _notifier.Error((exception as TomeltException).LocalizedMessage);
            }
            else if (_authorizer.Value.Authorize(StandardPermissions.SiteOwner)) {
                _notifier.Error(T(exception.Message));
            }
        }
    }
}
