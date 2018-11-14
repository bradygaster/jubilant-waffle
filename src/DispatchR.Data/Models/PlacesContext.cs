using System;
using System.Collections.Generic;
using System.Linq;
using GeoAPI.Geometries;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetTopologySuite.Geometries;

namespace DispatchR.Data.Models
{
    public class PlacesContext : DbContext
    {
        public PlacesContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Place> Places { get; set; }

        public void SetupDatabase(List<Place> places = null)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();

            if(places != null)
                AddRange(places);

            SaveChanges();
        }
    }
}