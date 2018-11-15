using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DispatchR.Data.Models;
using DispatchR.Api.Models;
using System.IO;
using Newtonsoft.Json.Linq;
using NetTopologySuite.Geometries;

namespace DispatchR.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ResetController : ControllerBase
    {
        public ResetController(PlacesContext placesContext)
        {
            PlacesDataContext = placesContext;
        }

        public PlacesContext PlacesDataContext { get; }

        // GET api/values
        [HttpGet]
        public ActionResult Get()
        {
            List<Place> places = new List<Place>();

            using(FileStream fs = 
                System.IO.File.Open("places.json", FileMode.Open))
            {
                foreach (var place in JToken.Parse(
                    new StreamReader(fs).ReadToEnd()).ToObject<dynamic[]>())
                {
                    Console.WriteLine($"{place.Name}: {place.Latitude} x {place.Longitude}");

                    places.Add(new Place {
                        Name = place.Name,
                        Id = Guid.NewGuid(),
                        Location = new Point((double)place.Longitude, 
                            (double)place.Latitude) {SRID=4326}
                    });
                }
            }

            PlacesDataContext.SetupDatabase(places);
            
            return Ok();
        }
    }
}
