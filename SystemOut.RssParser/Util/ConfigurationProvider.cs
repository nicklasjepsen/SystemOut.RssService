using System.Configuration;

namespace SystemOut.RssParser.Util
{
    public static class ConfigurationProvider
    {
        public const string FeedImportIntervalInSeconds = "FeedImportIntervalInSeconds";

        public static int GetIntValue(string key, int defaultValue)
        {
            int result;
            if (!int.TryParse(ConfigurationManager.AppSettings[key], out result))
                result = defaultValue;
            return result;
        }
    }
}
