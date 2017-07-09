using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tomelt.DisplayManagement.Descriptors;
using Tomelt.Environment.Extensions;

namespace Tomelt.Core.Contents {
    [TomeltFeature("Contents.ControlWrapper")]
    public class ControlWrapper : IShapeTableProvider {
        public void Discover(ShapeTableBuilder builder) {
            builder.Describe("Content").OnDisplaying(displaying => {
                if (!displaying.ShapeMetadata.DisplayType.Contains("Admin")) {
                    displaying.ShapeMetadata.Wrappers.Add("Content_ControlWrapper");
                }
            });
        }
    }
}