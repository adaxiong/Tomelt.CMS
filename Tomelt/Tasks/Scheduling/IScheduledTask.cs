using System;
using Tomelt.ContentManagement;

namespace Tomelt.Tasks.Scheduling {
    public interface IScheduledTask  {
        string TaskType { get; }
        DateTime? ScheduledUtc { get; }
        ContentItem ContentItem { get; }
    }
}