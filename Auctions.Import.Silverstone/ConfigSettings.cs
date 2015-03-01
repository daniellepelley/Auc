namespace Auctions.Import.Silverstone
{
    public static class ConfigSettings
    {
        public static string[] AuctionsListUrls
        {
            get
            {
                return new[]
                {
                    "https://www.silverstoneauctions.com/past-auctions"
                };
            }
        }
    }
}