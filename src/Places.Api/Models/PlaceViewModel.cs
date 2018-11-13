using System;

namespace Places.Api.Models 
{
    public class PlaceViewModel
    {
        public string Name { get; set; }
        public Guid Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}