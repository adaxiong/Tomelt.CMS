using System;
using System.Runtime.Serialization;
using Tomelt.Localization;

namespace Tomelt {
    [Serializable]
    public class TomeltException : ApplicationException {
        private readonly LocalizedString _localizedMessage;

        public TomeltException(LocalizedString message)
            : base(message.Text) {
            _localizedMessage = message;
        }

        public TomeltException(LocalizedString message, Exception innerException)
            : base(message.Text, innerException) {
            _localizedMessage = message;
        }

        protected TomeltException(SerializationInfo info, StreamingContext context)
            : base(info, context) {
        }

        public LocalizedString LocalizedMessage { get { return _localizedMessage; } }
    }
}