using System;
using System.Collections.Generic;
using System.Linq;
using Tomelt.Logging;
using Tomelt.UI.Notify;
using Tomelt.Exceptions;

namespace Tomelt.UI.Admin.Notification {
    public class NotificationManager : INotificationManager {
        private readonly IEnumerable<INotificationProvider> _notificationProviders;

        public NotificationManager(IEnumerable<INotificationProvider> notificationProviders) {
            _notificationProviders = notificationProviders;

            Logger = NullLogger.Instance;
        }

        public ILogger Logger { get; set; }

        public IEnumerable<NotifyEntry> GetNotifications() {
            return _notificationProviders
                .SelectMany(n => {
                    try {
                        return n.GetNotifications();
                    }
                    catch(Exception ex) {
                        if (ex.IsFatal()) {
                            throw;
                        } 
                        Logger.Error("An unhandled exception was thrown while generating a notification: " + n.GetType(), ex);
                        return Enumerable.Empty<NotifyEntry>();
                    }
                }).ToList();
        }
    }
}
