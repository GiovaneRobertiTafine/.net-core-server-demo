using CadastroSerie.DataAccess.DataObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroSerie.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class World : ControllerBase
    {

        WorldContext context = new WorldContext();

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<World> _logger;

        public World(ILogger<World> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("cities")]
        public IEnumerable<City> GetCities()
        {
            return this.context.Cities;
        }

        [HttpGet]
        [Route("city/{id:int}")]
        [ProducesResponseType(typeof(City), StatusCodes.Status200OK)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public City GetCity(int id)
        {
            var cities = context.Cities.AsQueryable();

            if (id.Equals(null))
            {
                return cities.FirstOrDefault(c => c.Id.Equals(id));
            } else
            {
                return null;
            }
        }

        [HttpGet("city")]
        public City GetCityName([FromQuery] string name)
        {
            var cities = context.Cities.AsQueryable();

            if (name != null)
            {
                return cities.FirstOrDefault(c => c.Name.Equals(name));
            }
            else
            {
                return null;
            }
        }
    }
}
