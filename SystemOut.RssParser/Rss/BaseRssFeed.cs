using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SystemOut.RssParser.Rss
{
	[Serializable]
	[XmlRoot("rss", IsNullable = false)]
	public class BaseRssFeed<TChannel> : IRssFeed<TChannel>
	{
		[XmlElement("channel")]
		public List<TChannel> RssChannels { get; set; }

		public virtual List<TChannel> GetRssChannels()
		{
			if (RssChannels == null)
			{
				return new List<TChannel>();
			}
			return RssChannels;
		}
	}
}