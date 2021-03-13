using AutoBeer.Data.Classes;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace AutoBeer.Data.Services
{
    public class BeerDbContext : DbContext
    {
        public DbSet<Beer> Beers { get; set; }
    }
}
