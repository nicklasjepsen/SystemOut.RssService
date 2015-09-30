using System;
using System.Threading;
using SystemOut.RssParser;
using SystemOut.RssParser.Model;

namespace SystemOut.RssParserConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var rssService = new NewsFeedParseService();
            rssService.OnNewFeedItems += RssService_OnNewFeedItems;
            rssService.AddFeedSource(new FeedSource { Url = "http://www.dr.dk/nyheder/service/feeds/allenyheder" });
            rssService.Execute(new CancellationToken()).Wait();

            Console.ReadKey();
        }

        private static void RssService_OnNewFeedItems(object sender, NewsFeedParseService.FeedItemEventArgs e)
        {
            foreach (var feedItem in e.Items)
            {
                Console.WriteLine(feedItem.Title);
            }
        }
    }
}
