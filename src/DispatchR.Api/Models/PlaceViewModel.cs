using System;
using DispatchR.Data.Models;

namespace DispatchR.Api.Models
{
    public static class PlaceViewModelExtensions
    {
        public static PlaceViewModel CreateViewModel(this Place x)
        {
            return new PlaceViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Latitude = x.Location.Y,
                Longitude = x.Location.X,
                Status = x.Status
            };
        }
    }

    public class PlaceViewModel
    {
        public string Name { get; set; }
        public Guid Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Status { get; set; }
    }
}