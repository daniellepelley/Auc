using System.Collections.Generic;
using System.Linq;
using Auctions.Model;

namespace Auctions.Export
{
    public class SalesExporter : ISalesExporter
    {
        private readonly AuctionEntities _auctionEntities;
        private readonly IUpdater<Sale> _updater;
        private List<Sale> _existingSales;
        private Getter<Model> _modelGetter;
        private Getter<Make> _makeGetter;

        public SalesExporter(AuctionEntities auctionEntities, IUpdater<Sale> updater)
        {
            _updater = updater;
            _auctionEntities = auctionEntities;
        }

        public void Export(AuctionSale[] auctionSales)
        {
            _modelGetter = new Getter<Model>(_auctionEntities, _auctionEntities.Models);
            _makeGetter = new Getter<Make>(_auctionEntities, _auctionEntities.Makes);

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
    }
}
