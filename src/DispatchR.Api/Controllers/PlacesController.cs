using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DispatchR.Data.Models;
using DispatchR.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace DispatchR.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PlacesController : ControllerBase
    {
        private PlacesContext _context;

        public PlacesController(PlacesContext placesContext)
        {
            _context = placesContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlaceViewModel>>> Get()
        {
            return await _context.Places.Select(x => x.CreateViewModel()).ToArrayAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PlaceViewModel>> Get(Guid id)
        {
            var place = await _context.Places.FindAsync(id);

            if (place == null)
            {
                return NotFound();
            }

            return place.CreateViewModel();
        }
    }
}
