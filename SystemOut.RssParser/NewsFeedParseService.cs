using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SystemOut.RssParser.Model;
using SystemOut.RssParser.Util;
using NLog;

namespace SystemOut.RssParser
{
    public class NewsFeedParseService
    {
        private readonly List<FeedSource> feedSources;
        private readonly IFeedParser defaultParser;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public delegate void NewFeedItemHandler(object sender, FeedItemEventArgs e);

        public event NewFeedItemHandler OnNewFeedItems;

        public class FeedItemEventArgs : EventArgs
        {
            public IEnumerable<FeedItem> Items { get; set; }
        }

        public NewsFeedParseService()
        {
            feedSources = new List<FeedSource>();
            defaultParser = new DefaultFeedParser();
        }

        public void AddFeedSource(FeedSource source)
        {
            feedSources.Add(source);
        }

        public async Task Execute(CancellationToken cancellationToken)
        {
            var feedItemsCache = new HashSet<string>();
            do
            {
                foreach (var feedSource in feedSources)
                {
                    Logger.Debug($"Reading feed from {feedSource.Url}.");
                    var newItems = new List<FeedItem>();
                    var items = await defaultParser.Parse(feedSource);
                    if (items == null)
                        continue;

                    foreach (var feedItem in items)
                    {
                        if (feedItemsCache.Contains(feedItem.ExternalItemId))
                            continue;
                        feedItemsCache.Add(feedItem.ExternalItemId);
                        newItems.Add(feedItem);
                    }

                    Logger.Debug($"Got {newItems.Count} new items.");

                    if (newItems.Count > 0)
                        OnNewFeedItems?.Invoke(this, new FeedItemEventArgs { Items = newItems });

                }

                Logger.Info($"Sleeping {ConfigurationProvider.GetIntValue(ConfigurationProvider.FeedImportIntervalInSeconds, 60)} second(s) before starting all over.");
                await Task.Delay(ConfigurationProvider.GetIntValue(ConfigurationProvider.FeedImportIntervalInSeconds, 60) * 1000, cancellationToken);
            } while (!cancellationToken.IsCancellationRequested);

            Logger.Info("Task is cancelled - escaping endless loop.");
        }
    }

}
