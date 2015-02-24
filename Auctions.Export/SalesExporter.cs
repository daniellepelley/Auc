﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using EntityFramework.BulkInsert.Extensions;

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

        public void Export(Auctions.Model.Sale[] sales)
        {
            _modelGetter = new Getter<Model>(_auctionEntities, _auctionEntities.Models);
            _makeGetter = new Getter<Make>(_auctionEntities, _auctionEntities.Makes);

            _existingSales = _auctionEntities.Sales.ToList();

            foreach (var sale in sales)
            {
                var make = GetMake(sale.Make);

                var model = GetModel(sale.Model, make);

                var dbSale = _existingSales
                    .FirstOrDefault(x =>
                        x.Model == model &&
                        x.Price == sale.Price);

                if (dbSale != null)
                {
                    continue;
                }

                dbSale = new Sale
                {
                    Price = sale.Price,
                    Model = model
                };
                _updater.Update(new[] {dbSale});
                _existingSales.Add(dbSale);
            }
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
