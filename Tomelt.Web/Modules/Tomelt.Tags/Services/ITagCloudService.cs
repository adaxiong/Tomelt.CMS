using System.Collections.Generic;
using Tomelt.Tags.Models;

namespace Tomelt.Tags.Services {
    public interface ITagCloudService : IDependency {
        IEnumerable<TagCount> GetPopularTags(int buckets, string slug);
    }
}