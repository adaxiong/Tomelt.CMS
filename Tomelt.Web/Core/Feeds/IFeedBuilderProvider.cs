using Tomelt.Core.Feeds.Models;

namespace Tomelt.Core.Feeds {
    public interface IFeedBuilderProvider : IDependency {
        FeedBuilderMatch Match(FeedContext context);
    }
    
    public class FeedBuilderMatch {
        public int Priority { get; set; }
        public IFeedBuilder FeedBuilder { get; set; }
    }
}
