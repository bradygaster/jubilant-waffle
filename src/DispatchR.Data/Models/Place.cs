using System;
using System.Collections.Generic;
using System.Linq;
using GeoAPI.Geometries;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NetTopologySuite.Geometries;

namespace DispatchR.Data.Models
{
    public class Place
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Point Location { get; set; }
    }
}