using System;
using System.Xml.Serialization;
using SystemOut.RssParser.Util;

namespace SystemOut.RssParser.Rss
{
    [Serializable]
    [XmlRoot("item", IsNullable = false)]
    public class BaseRssItem : IBaseRssItem
    {
        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("pubDate")]
        public string PublishedDate { get; set; }

        [XmlElement("link")]
        public string Link { get; set; }

        [XmlElement("guid")]
        public string Guid { get; set; }

        [XmlIgnore]
        public DateTime Date
        {
            get
            {
                DateTime date;
                if (DateTime.TryParse(PublishedDate, out date))
                    return date.ToUniversalTime();

                return DateTimeParser.ParseDanishRssDate(PublishedDate).ToUniversalTime();
            }
        }

        public virtual string GetGuid()
        {
            if (string.IsNullOrEmpty(Guid))
                return Link;
            return Guid;
        }
    }

    public interface IBaseRssItem
    {
        string GetGuid();
    }
}