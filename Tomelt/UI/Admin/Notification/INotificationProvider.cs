using System.Collections.Generic;
using Tomelt.UI.Notify;

namespace Tomelt.UI.Admin.Notification {
    public interface INotificationProvider : IDependency {
        /// <summary>
        /// Returns all notifications to display per zone
        /// </summary>
        IEnumerable<NotifyEntry> GetNotifications();
    }
}
