using System;
using System.Collections.Generic;
using System.Linq;
using imageDetect.Model;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace imageDetect.Controllers
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

        [HttpGet]
        public IEnumerable<Photos> Get()
        {
            Statistics statistics = new Statistics();
            statistics.Id = 1;
            statistics.Name = "Psy";
            statistics.NumberOfSearches = 1234;
            statistics.ZeroObjects = 32;
            statistics.AverageError = 0.45;
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new Photos
            {

                Id = 12,
                FoundNumber = 2,
                RealNumber = 3,
                ObjectID = 323,
                ObjectStatistics = statistics

                //Date = DateTime.Now.AddDays(index),
                //TemperatureC = rng.Next(-20, 55),
                //Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
