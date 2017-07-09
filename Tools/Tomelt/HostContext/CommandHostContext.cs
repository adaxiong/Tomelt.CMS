using System.IO;
using Tomelt.Host;

namespace Tomelt.HostContext {
    public class CommandHostContext {
        public CommandReturnCodes StartSessionResult { get; set; }
        public CommandReturnCodes RetryResult { get; set; }

        public TomeltParameters Arguments { get; set; }
        public DirectoryInfo TomeltDirectory { get; set; }
        public bool DisplayUsageHelp { get; set; }
        public CommandHost CommandHost { get; set; }
        public Logger Logger { get; set; }
    }
}