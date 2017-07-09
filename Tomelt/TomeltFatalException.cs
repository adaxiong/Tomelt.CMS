using System;
using System.Runtime.Serialization;
using Tomelt.Localization;

namespace Tomelt {
    [Serializable]
    public class TomeltFatalException : Exception {
        private readonly LocalizedString _localizedMessage;

        public TomeltFatalException(LocalizedString message)
            : base(message.Text) {
            _localizedMessage = message;
        }

        public TomeltFatalException(LocalizedString message, Exception innerException)
            : base(message.Text, innerException) {
            _localizedMessage = message;
        }

        protected TomeltFatalException(SerializationInfo info, StreamingContext context)
            : base(info, context) {
        }

        public LocalizedString LocalizedMessage { get { return _localizedMessage; } }
    }
}
