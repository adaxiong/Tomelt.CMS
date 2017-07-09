using System.Web;

namespace Tomelt.Time {
    public interface ITimeZoneSelector : IDependency {
        TimeZoneSelectorResult GetTimeZone(HttpContextBase context);
    }
}
