using System;
using Tomelt.ContentManagement;

namespace Tomelt.Tasks.Scheduling {
    public interface IPublishingTaskManager : IDependency {
        IScheduledTask GetPublishTask(ContentItem item);
        void Publish(ContentItem item, DateTime scheduledUtc);
        void DeleteTasks(ContentItem item);
    }
}