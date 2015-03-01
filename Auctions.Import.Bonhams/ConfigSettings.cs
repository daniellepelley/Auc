namespace Auctions.Import.Bonhams
{
    public static class ConfigSettings
    {
        public static string AuctionsListUrl
        {
            get
            {
                return
                    "https://www.bonhams.com/api/v1/search_json/?content=sale&date_range=past&department=MOT-CYC&length=5000&main_index_key=sale&page=1";
            }
        }
    }
}
