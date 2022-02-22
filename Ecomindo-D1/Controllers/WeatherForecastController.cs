using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecomindo_D1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Deletes a specific TodoItem.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
        /// <summary>
        ///  Insert Weather
        /// </summary>
        /// <param name="weather">Weather Data</param>
        /// <response code="200">Request ok.</response>
        /// <response code="400">Request Failed</response>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(WeatherForecast), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<WeatherForecast> InsertOne()
        {
            var result = new WeatherForecast
            {
                Date = DateTime.Now,

            };
            return result;
        }
    }
}
