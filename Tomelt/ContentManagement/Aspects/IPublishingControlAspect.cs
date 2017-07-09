using System;
using Tomelt.ContentManagement.Utilities;

namespace Tomelt.ContentManagement.Aspects {
    public interface IPublishingControlAspect {
        LazyField<DateTime?> ScheduledPublishUtc { get; }
    }
}