using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoBeer.Data.Classes;
using AutoBeer.Data.Enums;
using AutoBeer.Data.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AutoBeer.Api
{
    /// <summary>
    /// The BeerMeasurementController accepts new measurements for existing Beers
    /// </summary>
    [Route("api/[controller]")]
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

        private bool IsAuthenticated(bool isTilt = false)
        {
            if (isTilt && HttpContext.Request.Headers.TryGetValue("x-Requested-With", out var tiltheader))
            {
                //Currently, everyone with the correct header can supply measurements.
                //Maybe someday this changes...
                return tiltheader.First().Equals("com.baronbrew.tilthydrometerf7");
            }

            if (HttpContext.Session.TryGetValue("ApiKey", out var apiKey))
            {
                return _configuration.GetValue(typeof(string), "ApiKey").Equals(Encoding.Unicode.GetString(apiKey));
            }

            if (HttpContext.Request.Headers.TryGetValue("X-ApiKey", out var apiKeyHeader))
            {
                return _configuration.GetValue(typeof(string), "ApiKey").Equals(apiKeyHeader.First());
            }

            return false;
        }

        /// <summary>
        /// This methods is used to retrieve all available measurements
        /// </summary>
        /// <returns>IEnub</returns>
        // GET: api/BeerMeasurement
        [HttpGet("[action]/{beerId}")]
        public IEnumerable<BeerMeasurementInfo> Get(int beerId)
        {
            return _brewDb.GetMeasurements(beerId);
        }

        // GET: api/BeerMeasurement/5
        [HttpGet("[action]/{beerId}/{id}")]
        public BeerMeasurementInfo Get(int beerId, long id)
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
        // POST: api/BeerMeasurement
        [HttpPost]
        public void Post(double Timepoint, double SG, double Temp, TiltColor Color, string Beer, string Comment)
        {
            if (IsAuthenticated(true))
            {
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
            else
            {
                Response.StatusCode = 403;
            }

        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("[action]/{beerId}/{id}")]
        public void Delete(int beerId, long id)
        {
            if (IsAuthenticated())
            {
                _brewDb.DeleteMeasurement(beerId, id);
            }
            else
            {
                Response.StatusCode = 403;
            }
        }
    }
}
