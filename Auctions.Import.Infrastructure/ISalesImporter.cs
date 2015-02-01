namespace Auctions.Import.Infrastructure
{
    public interface ISalesImporter
    {
        Sale[] Import(string url);
    }
}