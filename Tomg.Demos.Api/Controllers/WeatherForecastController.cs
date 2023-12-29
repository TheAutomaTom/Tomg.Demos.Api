using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace Tomg.Demos.Api.Controllers
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Frigid", "Frosty", "Chill", "Coolio", "Mello", "Sh'lakable", "Sh'limey", "Haught", "Scorcher"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("{count}")]
        public IEnumerable<WeatherForecast> Get(int count)
        {
            var max = count > 0 ? count : 1;

            var forecasts = Enumerable.Range(1, max).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-30, 49),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();

            return forecasts;
        }
    }
}
