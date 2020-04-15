using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoBeer.Data.Classes;
using AutoBeer.Data.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AutoBeer.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeerMeasurementController : ControllerBase
    {
        private IConfiguration _configuration;
        private IBreweryData _brewDb;
        public BeerMeasurementController(IBreweryData db, IConfiguration configuration)
        {
            _configuration = configuration;
            _brewDb = db;
        }

        private bool IsAuthenticated()
        {
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
        // GET: api/BeerMeasurement
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/BeerMeasurement/5
        [HttpGet("[action]/{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/BeerMeasurement
        [HttpPost]
        public void Post(double Timepoint, double SG, double Temp, string Color, string Beer, string Comment)
        {

            /*
             * NUR WENN x-Requested-With com.baronbrew.tilthydrometerf7
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

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(long id, int beerId)
        {
            var timestamp = new DateTime(id); // Search and rmeove
            _brewDb.DeleteMeasurement(beerId, id);
        }
    }
}
