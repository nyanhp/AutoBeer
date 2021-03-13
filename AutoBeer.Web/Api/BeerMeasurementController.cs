using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoBeer.Data.Classes;
using AutoBeer.Data.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AutoBeer.Api
{
    /// <summary>
    /// The BeerMeasurementController accepts new measurements for existing Beers
    /// </summary>
    [ApiController]
    public class BeerMeasurementController : ControllerBase
    {
        private IConfiguration _configuration;
        private IBreweryData _brewDb;

        /// <summary>
        /// The constuctor uses the IoC pattern
        /// </summary>
        /// <param name="db">The database context</param>
        /// <param name="configuration">The configuration context</param>
        public BeerMeasurementController(IBreweryData db, IConfiguration configuration)
        {
            _configuration = configuration;
            _brewDb = db;
        }

        /// <summary>
        /// This methods is used to retrieve all available measurements
        /// </summary>
        /// <returns>IEnub</returns>
        // GET: api/BeerMeasurement
        [HttpGet("api/[controller]/{beerId}")]
        [HttpGet("api/{apikey?}/[controller]/{beerId}")]
        public IEnumerable<BeerMeasurementInfo> Get(string apikey, int beerId)
        {
            return _brewDb.GetMeasurements(beerId);
        }

        // GET: api/BeerMeasurement/5
        [HttpGet("api/[controller]/{beerId}/{id}")]
        [HttpGet("api/{apikey?}/[controller]/{beerId}/{id}")]
        public BeerMeasurementInfo Get(string apikey, int beerId, long id)
        {
            return _brewDb.GetMeasurement(beerId, id);
        }

        /// <summary>
        /// This methods posts new measurements
        /// </summary>
        /// <param name="Timepoint"></param>
        /// <param name="SG"></param>
        /// <param name="Temp"></param>
        /// <param name="Color"></param>
        /// <param name="Beer"></param>
        /// <param name="Comment"></param>
        /// <param name="apikey"></param>
        // POST: api/BeerMeasurement
        [HttpPost("api/{apikey}/[controller]")]
        public void Post(string apikey, double Timepoint, double SG, double Temp, string Color, string Beer, string Comment)
        {
            if (!_configuration.GetValue(typeof(string), "ApiKey").ToString().Equals(apikey)) Response.StatusCode = 403;
            /*
             * Weird calculations from https://raw.githubusercontent.com/baronbrew/TILTpi/Aioblescan/flow.json
             */
            var weirdTimezoneOffset = TimeZoneInfo.Local.GetUtcOffset(DateTime.Now).TotalMinutes / 24 / 60;
            var weirdResult = (Timepoint + weirdTimezoneOffset - 25569) * (1000 * 60 * 60 * 24);
            var timeStamp = (new DateTime(1970, 1, 1)).AddMilliseconds(weirdResult);

            var beerMeasure = new BeerMeasurementInfo()
            {
                BeerName = Beer,
                Timestamp = timeStamp,
                SpecificGravity = SG,
                TemperatureFahrenheit = Temp,
                TiltColor = Color,
                Comment = Comment
            };

            _brewDb.AddMeasurement(_brewDb.GetBeer(Beer).Id, beerMeasure);


        }

        /// <summary>
        /// This methods posts new measurements
        /// </summary>
        /// <param name="apikey"></param>
        /// <param name="beerId"></param>
        /// <param name="id"></param>
        // POST: api/BeerMeasurement
        [HttpDelete("api/{apikey}/[controller]/{beerId}/{id}")]
        public void Delete(string apikey, int beerId, long id)
        {
            if (!_configuration.GetValue(typeof(string), "ApiKey").ToString().Equals(apikey)) Response.StatusCode = 403;

            _brewDb.DeleteMeasurement(beerId, id);
        }
    }
}
