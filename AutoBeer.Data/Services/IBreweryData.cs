using AutoBeer.Data.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoBeer.Data.Services
{
    public interface IBreweryData
    {
        // Get, Add, Update, Delete

        //Get
        IEnumerable<Beer> GetAllBeers();

        Beer GetBeer(int id);
        Beer GetBeer(string name);

        IEnumerable<BeerMeasurementInfo> GetMeasurements(int beerId);
        BeerMeasurementInfo GetMeasurement(long measurementId);

        //Add
        void AddBeer(Beer beer);
        void AddMeasurement(int beerId, BeerMeasurementInfo info);

        //Update
        void UpdateBeer(Beer beer);
        void UpdateMeasurement(BeerMeasurementInfo info);

        //Delete
        void DeleteBeer(int id);
        void DeleteMeasurement(long measurementId);
    }
}
