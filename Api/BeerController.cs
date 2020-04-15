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
        [HttpGet]
        public IEnumerable<Beer> Get()
        {
            return _brewDb.GetAllBeers();
        }

        // GET: api/Beer/5
        [HttpGet("{id}")]
        public Beer Get(int id)
        {
            return _brewDb.GetBeer(id);
        }

        // POST: api/Beer
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
