using System.Collections.Generic;

namespace SystemOut.RssParser.Rss
{
	public interface IRssFeed<T>
	{
		List<T> GetRssChannels();
	}
}