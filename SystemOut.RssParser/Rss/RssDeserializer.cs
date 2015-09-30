using System;
using System.IO;
using System.Net;
using System.Xml;
using System.Xml.Serialization;
using NLog;

namespace SystemOut.RssParser.Rss
{
    /// <summary>
    /// Deserialize an RssFeed
    /// </summary>
    public static class RssDeserializer
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static BaseRssFeed<BaseRssChannel<BaseRssItem>> GetFeed(string feedUrl)
        {
            return GetFeed<BaseRssFeed<BaseRssChannel<BaseRssItem>>>(feedUrl);
        }

        /// <summary>
        /// Deserialize an RSS from an URL
        /// </summary>
        /// <typeparam name="T">The type of feed to deserialize into</typeparam>
        /// <param name="feedUrl">The location of the feed</param>
        /// <returns>The deserialized feed</returns>
        public static T GetFeed<T>(string feedUrl)
        {
            if (string.IsNullOrEmpty(feedUrl)) return default(T);

            var xs = new XmlSerializer(typeof(T));
            try
            {
                var xmlReaderSettings = new XmlReaderSettings
                {
                    DtdProcessing = DtdProcessing.Parse
                };
                T rss;
                using (var reader = XmlReader.Create(feedUrl, xmlReaderSettings))
                {
                    rss = (T)xs.Deserialize(reader);
                }
                return rss;
            }
            catch (WebException webException)
            {
                Logger.Log(LogLevel.Error, webException);
                return default(T);
            }
            catch (InvalidOperationException invalidOperationException)
            {
                Logger.Log(LogLevel.Error, invalidOperationException);
                return default(T);
            }
        }
    }
}