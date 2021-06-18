using CadastroSerie.DataAccess.DataObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        [ProducesResponseType(typeof(City), StatusCodes.Status200OK)]
        [ProducesResponseType(500)]
        public IActionResult GetCities()
        {
            try
            {
                City[] cities = this.context.Cities.ToArray();

                return Ok(cities);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }

        }

        [HttpGet]
        [Route("city/{id:int}")]
        [ProducesResponseType(typeof(City), StatusCodes.Status200OK)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult GetCity(int id)
        {
            try
            {
                var cities = context.Cities.AsQueryable();
                if (id != 0)
                {
                    City city = cities.FirstOrDefault(c => c.Id.Equals(id));

                    if (city == null)
                    {
                        return Ok(city);

                    }
                    else
                    {
                        return NotFound(new { message = "Não Encontrado" });
                    }
                }

                return StatusCode((int)HttpStatusCode.BadRequest);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("city")]
        [ProducesResponseType(typeof(City), StatusCodes.Status200OK)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult GetCityName([FromQuery] string name)
        {
            try
            {
                var cities = context.Cities.AsQueryable();
                if (name != null)
                {
                    City city = cities.FirstOrDefault(c => c.Name.Equals(name));

                    if (city != null)
                    {
                        return Ok(city);

                    }
                    else
                    {
                        return NotFound(new { message = "Não Encontrado" });
                    }
                }

                return StatusCode((int)HttpStatusCode.BadRequest);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
