using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Dynamic;

namespace GenericFromForm.Controllers
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

        [HttpGet(Name = "GetWeatherForecast")]
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


        [HttpPut("{id}")]
        public IActionResult Put([FromForm] dynamic template,int id)
        {
            dynamic results = JsonConvert.DeserializeObject<dynamic>(template);
            return Ok(results);
        }

        [HttpPost()]
        public IActionResult Example([FromForm] dynamic template)
        {
            // Utiliser le paramètre `template` comme un objet dynamic

            // Lire une propriété de l'objet dynamic
            string name = template.Name;

            // Modifier une propriété de l'objet dynamic
            template.Age = 30;

            // Créer un nouvel objet dynamic
            dynamic newTemplate = new ExpandoObject();
            newTemplate.Title = "New template";
            newTemplate.Content = "Template content";

            // Utiliser l'objet dynamic dans la réponse HTTP
            return Ok(newTemplate);
        }
    }
}