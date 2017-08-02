using System.Linq;
using System.Xml.Linq;
using Tomelt.ContentManagement;
using Tomelt.Core.Feeds;
using Tomelt.Core.Feeds.Models;
using Tomelt.Environment.Extensions;
using Tomelt.Tags.Models;

namespace Tomelt.Tags.Feeds {
    [TomeltFeature("Tomelt.Tags.Feeds")]
    public class TagsFeedItemBuilder : IFeedItemBuilder {
        public void Populate(FeedContext context) {
            foreach (var feedItem in context.Response.Items.OfType<FeedItem<ContentItem>>()) {
                // add to known formats
                if (context.Format == "rss") {
                    
                    // adding tags to the rss item
                    var tagsPart = feedItem.Item.As<TagsPart>();
                    if (tagsPart != null) {
                        tagsPart.CurrentTags.ToList().ForEach(x => 
                            feedItem.Element.Add(new XElement("category", x)));
                    }
                }
            }
        }
    }
}