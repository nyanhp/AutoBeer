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
    [Route("api/[controller]")]
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

        // GET: api/Beer
        /// <summary>
        /// Get all the beer!
        /// </summary>
        /// <returns>An IEnumerable of Beer</returns>
        /// <response code="200">All good</response>
        /// <response code="403">Indicates that you did not use an API key in the header X-ApiKey or that the key did not match what was configured in your appsettings</response>
        [HttpGet]
        public IEnumerable<Beer> Get()
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
        [HttpGet("{id}")]
        public Beer Get(int id)
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
        [HttpPost]
        public void Post([FromBody] Beer beer)
        {
            if (IsAuthenticated())
            {
                _brewDb.AddBeer(beer);
            }
            else
            {
                Response.StatusCode = 403;
            }
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
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Beer beer)
        {
            if (IsAuthenticated())
            {
                _brewDb.UpdateBeer(id, beer);
            }
            else
            {
                Response.StatusCode = 403;
            }
        }

        // DELETE: api/ApiWithActions/5
        /// <summary>
        /// Delete a beer from the cellar
        /// </summary>
        /// <param name="id">The id of the beer to delete</param>
        /// <response code="200">All good</response>
        /// <response code="403">Indicates that you did not use an API key in the header X-ApiKey or that the key did not match what was configured in your appsettings</response>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            if (IsAuthenticated())
            {
                _brewDb.DeleteBeer(id);
            }
            else
            {
                Response.StatusCode = 403;
            }
        }
    }
}
