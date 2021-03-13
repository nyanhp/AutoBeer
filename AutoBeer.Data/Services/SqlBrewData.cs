using AutoBeer.Data.Classes;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace AutoBeer.Data.Services
{
    public class SqlBrewData : IBreweryData
    {
        private readonly BeerDbContext db;

        public SqlBrewData(BeerDbContext beerDbContext)
        {
            this.db = beerDbContext;
        }
        public void AddBeer(Beer beer)
        {
            db.Beers.Add(beer);
            db.SaveChanges();
        }

        public void AddMeasurement(int beerId, BeerMeasurementInfo info)
        {
            throw new NotImplementedException();
        }

        public void DeleteBeer(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteMeasurement(long measurementId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Beer> GetAllBeers()
        {
            return db.Beers;
        }

        public Beer GetBeer(int id)
        {
            return db.Beers.FirstOrDefault(beer => beer.Id == id);
        }

        public Beer GetBeer(string name)
        {
            return db.Beers.FirstOrDefault(beer => beer.Name == name);
        }

        public BeerMeasurementInfo GetMeasurement(long measurementId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BeerMeasurementInfo> GetMeasurements(int beerId)
        {
            throw new NotImplementedException();
        }

        public void UpdateBeer(Beer beer)
        {
            var beerToUpdate = db.Entry(beer);
            beerToUpdate.State = EntityState.Modified;
            db.SaveChanges();
        }

        public void UpdateMeasurement(BeerMeasurementInfo info)
        {
            throw new NotImplementedException();
        }
    }
}
