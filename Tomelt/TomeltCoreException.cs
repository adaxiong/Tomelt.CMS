using System;
using System.Runtime.Serialization;
using Tomelt.Localization;

namespace Tomelt {
    [Serializable]
    public class TomeltCoreException : Exception {
        private readonly LocalizedString _localizedMessage;

        public TomeltCoreException(LocalizedString message)
            : base(message.Text) {
            _localizedMessage = message;
        }

        public TomeltCoreException(LocalizedString message, Exception innerException)
            : base(message.Text, innerException) {
            _localizedMessage = message;
        }

        protected TomeltCoreException(SerializationInfo info, StreamingContext context)
            : base(info, context) {
        }

        public LocalizedString LocalizedMessage { get { return _localizedMessage; } }
    }
}