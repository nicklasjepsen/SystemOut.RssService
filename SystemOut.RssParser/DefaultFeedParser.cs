using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemOut.RssParser.Model;
using SystemOut.RssParser.Rss;

namespace SystemOut.RssParser
{
    public class DefaultFeedParser : IFeedParser
    {
        public async Task<List<FeedItem>> Parse(FeedSource source)
        {
            var feed = await Task.Run(() => RssDeserializer.GetFeed(source.Url));
            var channel = feed?.GetRssChannels()?.FirstOrDefault();
            if (channel == null)
                return new List<FeedItem>();
            return (from rssItem in channel.GetRssItems()
                    select new FeedItem
                    {
                        Title = rssItem.Title,
                        Url = rssItem.Link,
                        ExternalItemId = rssItem.GetGuid(),
                        ImportTime = DateTime.UtcNow,
                        PublishTime = rssItem.Date,
                        FeedSource = source,
                        Summary = rssItem.Description,
                    }).ToList();
        }
    }
}
