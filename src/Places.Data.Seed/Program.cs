using System;
using System.IO;
using GeoAPI.Geometries;
using NetTopologySuite.Geometries;
using Newtonsoft.Json.Linq;
using Places.Data.Models;
using System.Collections;
using System.Collections.Generic;

namespace Seed
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Place> places = new List<Place>();

            using(FileStream fs = File.Open("src/Places.Data/Seed/places.json", FileMode.Open))
            {
                foreach (var place in JToken.Parse(new StreamReader(fs).ReadToEnd()).ToObject<dynamic[]>())
                {
                    Console.WriteLine($"{place.Name}: {place.Latitude} x {place.Longitude}");

                    places.Add(new Place {
                        Name = place.Name,
                        Id = Guid.NewGuid(),
                        Location = new Point((double)place.Longitude, (double)place.Latitude) {SRID=4326}
                    });
                }
            }

            // using(var context = new PlacesContext())
            // {
            //     context.SetupDatabase(places);
            // }
        }
    }
}
