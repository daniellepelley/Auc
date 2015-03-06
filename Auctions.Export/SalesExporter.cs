using System.Collections.Generic;
using System.Linq;
using Auction.Reporting.DomainModel;
using Auctions.Data.Ef;
using Auctions.DomainModel;

namespace Auctions.Export
{
    public class SalesExporter : ISalesExporter
    {
        private readonly AuctionEntities _auctionEntities;
        private readonly IUpdater<Sale> _updater;
        private List<Sale> _existingSales;
        private Getter<Model> _modelGetter;
        private Getter<Make> _makeGetter;
        private Getter<AuctionHouse> _auctionHouseGetter;
        private Getter<Auction.Reporting.DomainModel.Auction> _auctionGetter;

        public SalesExporter(AuctionEntities auctionEntities, IUpdater<Sale> updater)
        {
            _updater = updater;
            _auctionEntities = auctionEntities;
        }

        public void Export(AuctionSale[] auctionSales)
        {
            _modelGetter = new Getter<Model>(_auctionEntities, _auctionEntities.Models);
            _makeGetter = new Getter<Make>(_auctionEntities, _auctionEntities.Makes);

            _auctionHouseGetter = new Getter<AuctionHouse>(_auctionEntities, _auctionEntities.AuctionHouses);
            _auctionGetter = new Getter<Auction.Reporting.DomainModel.Auction>(_auctionEntities, _auctionEntities.Auctions);

            _existingSales = _auctionEntities.Sales.ToList();

            var newSales = new List<Sale>();

            foreach (var auctionSale in auctionSales)
            {
                ProcessSale(auctionSale, newSales);
            }

            _updater.Update(newSales);
        }

        private void ProcessSale(AuctionSale auctionSale, List<Sale> newSales)
        {
            var make = GetMake(auctionSale.Make);

            var model = GetModel(auctionSale.Model, make);

            
            var dbSale = _existingSales
                .FirstOrDefault(x =>
                    x.Model == model &&
                    x.Price == auctionSale.Price);

            if (dbSale != null)
            {
                return;
            }

            var sale = new Sale
            {
                Price = auctionSale.Price,
                Model = model
            };

            var auctionHouse = GetAuctionHouse("Coys");

            if (auctionSale.AuctionListing != null)
            {
                var auction = GetAuction(auctionSale.AuctionListing.Name, auctionHouse);
                sale.Auction = auction;
            }

            newSales.Add(sale);
            _existingSales.Add(sale);
        }

        public Model GetModel(string modelName, Make make)
        {
            return _modelGetter.Get(
                x => x.Name == modelName,
                () => new Model
                {
                    Name = modelName,
                    Make = make
                });
        }

        private Make GetMake(string makeName)
        {
            return _makeGetter.Get(
                x => x.Name == makeName,
                () => new Make
                {
                    Name = makeName,
                });
        }

        public Auction.Reporting.DomainModel.Auction GetAuction(string modelName, AuctionHouse auctionHouse)
        {
            return _auctionGetter.Get(
                x => x.Name == modelName,
                () => new Auction.Reporting.DomainModel.Auction
                {
                    Name = modelName,
                    AuctionHouse = auctionHouse
                });
        }

        private AuctionHouse GetAuctionHouse(string auctionListingName)
        {
            return _auctionHouseGetter.Get(
                x => x.Name == auctionListingName,
                () => new AuctionHouse
                {
                    Name = auctionListingName,
                });
        }
    }
}
