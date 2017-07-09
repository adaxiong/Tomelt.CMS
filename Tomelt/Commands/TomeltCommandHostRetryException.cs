using System;
using System.Runtime.Serialization;
using Tomelt.Localization;

namespace Tomelt.Commands {
    [Serializable]
    public class TomeltCommandHostRetryException : TomeltCoreException {
        public TomeltCommandHostRetryException(LocalizedString message)
            : base(message) {
        }

        public TomeltCommandHostRetryException(LocalizedString message, Exception innerException)
            : base(message, innerException) {
        }

        protected TomeltCommandHostRetryException(SerializationInfo info, StreamingContext context)
            : base(info, context) {
        }
    }
}