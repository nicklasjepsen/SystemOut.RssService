using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemOut.RssParser.Model
{
    public class FeedItem
    {
        public string ExternalItemId { get; set; }

        public string Title { get; set; }
        public string Url { get; set; }
        public string Summary { get; set; }
        public string ImageUrl { get; set; }
        public DateTime PublishTime { get; set; }
        public DateTime ImportTime { get; set; }
        public FeedSource FeedSource { get; set; }
    }
}
