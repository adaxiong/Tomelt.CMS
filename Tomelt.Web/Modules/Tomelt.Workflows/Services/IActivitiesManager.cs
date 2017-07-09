using System.Collections.Generic;

namespace Tomelt.Workflows.Services {
    public interface IActivitiesManager : IDependency {
        IEnumerable<IActivity> GetActivities();
        IActivity GetActivityByName(string name);
    }

}