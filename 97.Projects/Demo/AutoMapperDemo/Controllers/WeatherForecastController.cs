using AutoMapperDemo.Models;
using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.Extensions.Hosting.IHostingEnvironment;

namespace AutoMapperDemo.Controllers
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
        private readonly IHostingEnvironment _hostingEnvironment;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IHostingEnvironment hostingEnvironment)
        {
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            string path1 = AppContext.BaseDirectory;

            string path2 = Path.GetDirectoryName(typeof(Program).Assembly.Location);

            Console.WriteLine();
            Console.WriteLine(path1);
            Console.WriteLine();
            Console.WriteLine(path2);
            Console.WriteLine();

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}