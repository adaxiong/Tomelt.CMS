using Tomelt.Localization;

namespace Tomelt.UI.Notify {
    public enum NotifyType {
        Information,
        Warning,
        Error
    }

    public class NotifyEntry {
        public NotifyType Type { get; set; }
        public LocalizedString Message { get; set; }
    }
}