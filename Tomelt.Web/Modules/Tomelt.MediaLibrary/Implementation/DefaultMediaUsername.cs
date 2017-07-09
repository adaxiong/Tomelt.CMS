using System;
using Tomelt.ContentManagement;
using Tomelt.MediaLibrary.Providers;
using Tomelt.Security;

namespace Tomelt.MediaLibrary.Implementation {
    public class DefaultMediaUsername : IMediaFolderProvider {
        public virtual string GetFolderName(IUser content) {
                string folder = "";
                foreach (char c in content.UserName) {
                    if (char.IsLetterOrDigit(c)) {
                        folder += c;
                    }
                    else
                        folder += "_" + String.Format("{0:X}", Convert.ToInt32(c));
                }
                return folder;
        }
    }
}