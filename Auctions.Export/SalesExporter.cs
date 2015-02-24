using System.Linq;

namespace Auctions.Export
{
    public class SalesExporter : ISalesExporter
    {
        private readonly AuctionEntities _auctionEntities;
        private Make[] _makes;
        private Model[] _models;

        public SalesExporter(AuctionEntities auctionEntities)
        {
            _auctionEntities = auctionEntities;
        }

        public void Export(Auctions.Model.Sale[] sales)
        {
            _makes = _auctionEntities.Makes.ToArray();
            _models = _auctionEntities.Models.ToArray();

            foreach (var sale in sales)
            {
                var make = GetMake(sale);
                var model = GetModel(sale, make);
                var dbSale = new Sale
                {
                    Price = sale.Price
                };

                model.Sales.Add(dbSale);
                _auctionEntities.SaveChanges();
            }

        }

        private Make GetMake(Auctions.Model.Sale sale)
        {
            var make = _makes.FirstOrDefault(x => x.Name == sale.Make);

            if (make == null)
            {
                make = new Make
                {
                    Name = sale.Make
                };
                _auctionEntities.Makes.Add(make);
            }
            return make;
        }

        private Model GetModel(Auctions.Model.Sale sale, Make make)
        {
            var model = _models.FirstOrDefault(x => x.Name == sale.Model);

            if (model == null)
            {
                model = new Model
                {
                    Name = sale.Model
                };
                make.Models.Add(model);
            }
            return model;
        }
    }
}
