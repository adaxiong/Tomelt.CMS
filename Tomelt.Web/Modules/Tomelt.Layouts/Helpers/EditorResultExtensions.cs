using System;
using System.Collections.Generic;
using System.Linq;
using Tomelt.Layouts.Framework.Drivers;
using Tomelt.Layouts.Models;
using Tomelt.Layouts.Services;

namespace Tomelt.Layouts.Helpers {
    public static class EditorResultExtensions {
        public static IEnumerable<string> CollectTabs(this EditorResult editorResult) {
            var set = new HashSet<ShapePosition>();

            foreach (var editor in editorResult.Editors) {
                var positionText = editor.Metadata.Position;

                if (!String.IsNullOrWhiteSpace(positionText)) {
                    var position = ShapePosition.Parse(positionText);
                    set.Add(position);
                }
            }
            return set.Distinct(new ShapePositionDistinctComparer()).OrderBy(x => x.Position).Select(x => x.Name);
        }
    }
}