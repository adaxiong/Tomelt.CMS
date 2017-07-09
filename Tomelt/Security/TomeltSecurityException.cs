using System;
using System.Runtime.Serialization;
using Tomelt.ContentManagement;
using Tomelt.Localization;

namespace Tomelt.Security {
    [Serializable]
    public class TomeltSecurityException : TomeltCoreException {
        public TomeltSecurityException(LocalizedString message) : base(message) { }
        public TomeltSecurityException(LocalizedString message, Exception innerException) : base(message, innerException) { }
        protected TomeltSecurityException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public string PermissionName { get; set; }
        public IUser User { get; set; }
        public IContent Content { get; set; }
    }
}
