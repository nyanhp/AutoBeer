using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoBeer.Data.Classes;
using AutoBeer.Data.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AutoBeer.Api
{
    /// <summary>
    /// This API controller is responsible for serving and storing Beer
    /// </summary>
    [ApiController]
    public class BeerController : ControllerBase
    {
        private IConfiguration _configuration;
        private IBreweryData _brewDb;
        public BeerController(IBreweryData db, IConfiguration configuration)
        {
            _configuration = configuration;
            _brewDb = db;
        }

        // GET: api/Beer
        /// <summary>
        /// Get all the beer!
        /// </summary>
        /// <returns>An IEnumerable of Beer</returns>
        /// <response code="200">All good</response>
        /// <response code="403">Indicates that you did not use an API key in the header X-ApiKey or that the key did not match what was configured in your appsettings</response>
        [HttpGet("api/[controller]")]
        [HttpGet("api/{apikey?}/[controller]")]
        public IEnumerable<Beer> Get(string apikey)
        {
            return _brewDb.GetAllBeers();
        }

        // GET: api/Beer/5
        /// <summary>
        /// Get one beer
        /// </summary>
        /// <param name="id">The id of the beer</param>
        /// <returns>The desired beer</returns>
        /// <response code="200">All good</response>
        /// <response code="403">Indicates that you did not use an API key in the header X-ApiKey or that the key did not match what was configured in your appsettings</response>
        [HttpGet("api/[controller]/{id}")]
        [HttpGet("api/{apikey?}/[controller]/{id}")]
        public Beer Get(string apikey, int id)
        {
            return _brewDb.GetBeer(id);
        }

        // POST: api/Beer
        /// <summary>
        /// Add a new beer to the cellar
        /// </summary>
        /// <param name="beer">This method expects a Beer object in the POST body</param>
        /// <remarks>Measurements is optional</remarks>
        /// <response code="200">All good</response>
        /// <response code="403">Indicates that you did not use an API key in the header X-ApiKey or that the key did not match what was configured in your appsettings</response>
        [HttpPost("api/{apikey}/[controller]")]
        public void Post(string apikey, [FromBody] Beer beer)
        {
            if (!_configuration.GetValue(typeof(string), "ApiKey").ToString().Equals(apikey)) Response.StatusCode = 403;
            _brewDb.AddBeer(beer);
        }

        // PUT: api/Beer/5
        /// <summary>
        /// Update a given beer, e.g. lowering the number of bottles remaining
        /// </summary>
        /// <param name="id">The id of the beer to update</param>
        /// <param name="beer">The Beer object to store instead of Id</param>
        /// <remarks>Measurements is optional</remarks>
        /// <response code="200">All good</response>
        /// <response code="403">Indicates that you did not use an API key in the header X-ApiKey or that the key did not match what was configured in your appsettings</response>
        [HttpPut("api/{apikey}/[controller]/{id}")]
        public void Put(string apikey, int id, [FromBody] Beer beer)
        {
            if (!_configuration.GetValue(typeof(string), "ApiKey").ToString().Equals(apikey)) Response.StatusCode = 403;
            _brewDb.UpdateBeer(id, beer);
        }

        // DELETE: api/ApiWithActions/5
        /// <summary>
        /// Delete a beer from the cellar
        /// </summary>
        /// <param name="id">The id of the beer to delete</param>
        /// <response code="200">All good</response>
        /// <response code="403">Indicates that you did not use an API key in the header X-ApiKey or that the key did not match what was configured in your appsettings</response>
        [HttpDelete("api/{apikey}/[controller]/{id}")]
        public void Delete(string apikey, int id)
        {
            if (!_configuration.GetValue(typeof(string), "ApiKey").ToString().Equals(apikey)) Response.StatusCode = 403;
            _brewDb.DeleteBeer(id);
        }
    }
}
