using System;
using System.Collections.Generic;
using System.Linq;
using GeoAPI.Geometries;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetTopologySuite.Geometries;

namespace Places.Data.Models
{
    public class PlacesContext : DbContext
    {
        private static readonly ILoggerFactory _loggerFactory = new LoggerFactory()
            .AddConsole((s, l) => l == LogLevel.Information && s.EndsWith("Command"));
        public DbSet<Place> Places { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(@"Server=tcp:localhost,1433;Initial Catalog=Places;User ID=sa;Password=P@55w0rd;ConnectRetryCount=0;",
                        sqlOptions => sqlOptions.UseNetTopologySuite())
                .UseLoggerFactory(_loggerFactory);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Place>().Property(b => b.Location).HasColumnType("Geography");
        }

        public void SetupDatabase(List<Place> places = null)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();

            if(places != null)
                AddRange(places);

            SaveChanges();
        }
    }

    public class Place
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Point Location { get; set; }
    }
}