using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SystemOut.RssParser.Rss
{
	[Serializable]
	[XmlRoot("channel", IsNullable = false)]
	public class BaseRssChannel<TItem> : IRssChannel<TItem>
	{
		[XmlElement("title")]
		public string Title { get; set; }

		[XmlElement("description")]
		public string Description { get; set; }

		[XmlElement("link")]
		public string Link { get; set; }

		[XmlElement("item")]
		public List<TItem> RssItems { get; set; }

        [XmlElement("generator")]
        public string Generator { get; set; }

        [XmlElement("image")]
        public RssImage Image { get; set; }

		public virtual List<TItem> GetRssItems()
		{
			if (RssItems == null)
			{
				return new List<TItem>();
			}
			return RssItems;
		}
	}
}