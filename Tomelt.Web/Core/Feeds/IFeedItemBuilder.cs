using Tomelt.Core.Feeds.Models;
using Tomelt.Events;

namespace Tomelt.Core.Feeds {
    public interface IFeedItemBuilder : IEventHandler {
        void Populate(FeedContext context);
    }
}
