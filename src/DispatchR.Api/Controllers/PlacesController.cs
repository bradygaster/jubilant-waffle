﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DispatchR.Data.Models;
using DispatchR.Api.Models;

namespace DispatchR.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlacesController : ControllerBase
    {
        public PlacesController(PlacesContext placesContext)
        {
            PlacesDataContext = placesContext;
        }

        public PlacesContext PlacesDataContext { get; }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<PlaceViewModel>> Get()
        {
            return PlacesDataContext.Places.ToArray().Select(x => 
                new PlaceViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Latitude = x.Location.Y,
                    Longitude = x.Location.X 
                }).ToArray();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Place> Get(Guid id)
        {
            return PlacesDataContext.Places.First(x => x.Id == id);
        }
    }
}