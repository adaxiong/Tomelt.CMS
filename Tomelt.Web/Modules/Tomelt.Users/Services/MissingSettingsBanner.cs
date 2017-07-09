using System.Collections.Generic;
using Tomelt.ContentManagement;
using Tomelt.Localization;
using Tomelt.Messaging.Services;
using Tomelt.UI.Admin.Notification;
using Tomelt.UI.Notify;
using Tomelt.Users.Models;

namespace Tomelt.Users.Services {
    public class MissingSettingsBanner : INotificationProvider {
        private readonly ITomeltServices _tomeltServices;
        private readonly IMessageChannelManager _messageManager;

        public MissingSettingsBanner(ITomeltServices tomeltServices, IMessageChannelManager messageManager) {
            _tomeltServices = tomeltServices;
            _messageManager = messageManager;
            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        public IEnumerable<NotifyEntry> GetNotifications() {

            var registrationSettings = _tomeltServices.WorkContext.CurrentSite.As<RegistrationSettingsPart>();

            if ( registrationSettings != null &&
                    ( registrationSettings.UsersMustValidateEmail ||
                    registrationSettings.NotifyModeration ||
                    registrationSettings.EnableLostPassword ) &&
                null == _messageManager.GetMessageChannel("Email", new Dictionary<string, object> {
                    {"Body", ""}, 
                    {"Subject", "Subject"},
                    {"Recipients", "john.doe@outlook.com"}
                }) ) {
                yield return new NotifyEntry { Message = T("Some Tomelt.User settings require an Email channel to be enabled."), Type = NotifyType.Warning };
            }
        }
    }
}
