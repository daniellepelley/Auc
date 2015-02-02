namespace Auctions.Model
{
    public interface ISalesImporter
    {
        Sale[] Import(string url);
    }
}