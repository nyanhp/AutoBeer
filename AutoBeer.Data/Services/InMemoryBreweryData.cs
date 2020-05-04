using AutoBeer.Data.Classes;
using System.Collections.Generic;
using System.Linq;

namespace AutoBeer.Data.Services
{
    public class InMemoryBreweryData : IBreweryData
    {
        List<Beer> _beers;
        public InMemoryBreweryData()
        {
            _beers = new List<Beer>()
                {
                    new Beer {Id = 0, Name = "Sierra Nevada Pale Ale", Style = Enums.BeerStyle.PaleAle, BitternessUnits = 35, Color = 25, OriginalGravity = 1.055, FinalGravity = 1.009, BatchSizeLiters = 20.4,Bottled = new System.DateTime(2020, 4,10), Brewed = new System.DateTime(2020,4,4),TotalBottles = 40, RemainingBottles = 37  },
                    new Beer {Id = 1, Name = "Amber", Style = Enums.BeerStyle.AmberAle, BitternessUnits = 20, Color = 20, OriginalGravity = 1.048, FinalGravity = 1.014, BatchSizeLiters = 20.4,Bottled = new System.DateTime(2018, 12,1), Brewed = new System.DateTime(2018,11,21),TotalBottles = 40, RemainingBottles = 0},
                    new Beer {Id = 2, Name = "Premium Pils", Style = Enums.BeerStyle.Pilsener, BitternessUnits = 35, Color = 7, OriginalGravity = 1.048, FinalGravity = 1.014, BatchSizeLiters = 20.4,Bottled = new System.DateTime(2019, 6,7), Brewed = new System.DateTime(2019,5,31),TotalBottles = 40, RemainingBottles = 0},
                };
        }

        public void DeleteBeer(int id)
        {
            var beerToRemove = GetBeer(id);
            if (null == beerToRemove) return;

            _beers.Remove(beerToRemove);
        }

        public Beer GetBeer(int id)
        {
            return _beers.FirstOrDefault(beer => beer.Id == id);
        }

        public IEnumerable<Beer> GetAllBeers()
        {
            return _beers.OrderBy(beer => beer.Name);
        }

        public void AddBeer(Beer beer)
        {
            var nextId = _beers.Max(b => b.Id) + 1;
            beer.Id = nextId;
            _beers.Add(beer);
        }

        public void UpdateBeer(int id, Beer beer)
        {
            var beerToUpdate = GetBeer(id);
            if (null == beerToUpdate) return;
            DeleteBeer(id);
            beer.Id = id;
            AddBeer(beer);
        }

        public IEnumerable<BeerMeasurementInfo> GetMeasurements(int beerId)
        {
            return GetBeer(beerId).Measurements;
        }

        public void AddMeasurement(int beerId, BeerMeasurementInfo info)
        {
            GetBeer(beerId).Measurements.Add(info);
        }

        public void UpdateMeasurement(int beerId, long measurementId, BeerMeasurementInfo info)
        {
            var beerToUpdate = GetBeer(beerId);
            if (null == beerToUpdate) return;

            var measurementToUpdate = GetMeasurement(beerId, measurementId);
            if (null == measurementToUpdate) return;
            DeleteMeasurement(beerId, measurementId);
            AddMeasurement(beerId, info);
        }

        public void DeleteMeasurement(int beerId, long measurementId)
        {
            var info = GetMeasurement(beerId, measurementId);
            GetBeer(beerId)?.Measurements.Remove(info);
        }

        public BeerMeasurementInfo GetMeasurement(int beerId, long measurementId)
        {
            return GetBeer(beerId).Measurements.FirstOrDefault(measure => measure.Id == measurementId);
        }

        public Beer GetBeer(string name)
        {
            return _beers.FirstOrDefault(beer => beer.Name.Equals(name));
        }
    }
}
