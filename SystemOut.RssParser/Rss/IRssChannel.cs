using System.Collections.Generic;

namespace SystemOut.RssParser.Rss
{
	public interface IRssChannel<T>
	{
		List<T> GetRssItems();
	}
}