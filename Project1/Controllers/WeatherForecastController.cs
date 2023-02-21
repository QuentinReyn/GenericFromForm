using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Dynamic;

namespace Project1.Controllers
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
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost]
        public IActionResult Example([FromForm] TestViewModel file)
        {
            // Utiliser le paramètre `template` comme un objet dynamic
            //var test = template.GetType().GetProperties();
            // Lire une propriété de l'objet dynamic
            //string name = template.Name;
            var data = JObject.Parse(file.Data);
            dynamic d = JsonConvert.DeserializeObject<dynamic>(file.Data);
            var test = file;
            // Modifier une propriété de l'objet dynamic

            // Créer un nouvel objet dynamic
            dynamic newTemplate = new ExpandoObject();
            newTemplate.Title = "New template";
            newTemplate.Content = "Template content";

            // Utiliser l'objet dynamic dans la réponse HTTP
            return Ok(newTemplate);
        }
    }
}