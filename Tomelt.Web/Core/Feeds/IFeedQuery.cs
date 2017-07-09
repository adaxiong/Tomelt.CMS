using Tomelt.Core.Feeds.Models;

namespace Tomelt.Core.Feeds {
    public interface IFeedQuery {
        void Execute(FeedContext context);
    }
}