using System.Collections.Generic;
using System.Threading.Tasks;
using SystemOut.RssParser.Model;

namespace SystemOut.RssParser
{
    public interface IFeedParser
    {
        Task<List<FeedItem>> Parse(FeedSource source);
    }
}