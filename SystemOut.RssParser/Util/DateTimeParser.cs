using System;
using System.Globalization;
using NLog;

namespace SystemOut.RssParser.Util
{
    public class DateTimeParser
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static DateTime ParseDanishRssDate(string date)
        {
            const string format = "ddd, dd MMM yyyy HH:mm:ss zzz";

            if (date.IndexOf(",", StringComparison.Ordinal) == 3)
            {
                // eg. fre, - we need just fr
                date = date.Substring(0, 2) + date.Substring(date.IndexOf(",", StringComparison.Ordinal));
            }

            DateTime result;
            if (DateTime.TryParseExact(date, format, CultureInfo.GetCultureInfo("da-DK"), DateTimeStyles.None,
                out result))
                return result;

            Logger.Warn($"Unable to parse date {date}.");
            return result;
        }
    }
}
