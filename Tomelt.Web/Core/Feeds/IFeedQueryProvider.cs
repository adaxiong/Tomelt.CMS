using Tomelt.Core.Feeds.Models;

namespace Tomelt.Core.Feeds {
    public interface IFeedQueryProvider : IDependency {
        FeedQueryMatch Match(FeedContext context);
    }

    public class FeedQueryMatch {
        public int Priority { get; set; }
        public IFeedQuery FeedQuery { get; set; }
    }
}
