using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tomelt.DisplayManagement.Descriptors;
using Tomelt.Environment.Extensions;

namespace Tomelt.Widgets {
    [TomeltFeature("Tomelt.Widgets.ControlWrapper")]
    public class ControlWrapper : IShapeTableProvider {
        public void Discover(ShapeTableBuilder builder) {
            builder.Describe("Widget")
                .Configure(descriptor => {
                    descriptor.Wrappers.Add("Widget_ControlWrapper");
                });
        }
    }
}