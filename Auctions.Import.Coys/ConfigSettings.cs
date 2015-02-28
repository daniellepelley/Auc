namespace Auctions.Import.Coys
{
    public static class ConfigSettings
    {
        public static string[] AuctionsListUrls
        {
            get
            {
                return new[]
                {
                    "http://www.coys.co.uk/past-auctions.php",
                    "http://www.coys.co.uk/past-auctions-old.php"
                };
            }
        }
    }
}