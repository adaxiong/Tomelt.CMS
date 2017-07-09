using System.Collections.Generic;
using Tomelt.Layouts.Framework.Display;

namespace Tomelt.Layouts.Helpers {
    public static class ElementDisplayContextHelper {
        
        public static IDictionary<string, object> GetTokenData(this ElementDisplayingContext context) {
            var data = new Dictionary<string, object>();

            if (context.Content != null)
                data["Content"] = context.Content.ContentItem;

            return data;
        }
    }
}