using Microsoft.AspNetCore.Mvc;
using MultipleChoise.Server.Data.Models.Entity;
using MultipleChoise.Server.Data.Repositorys;

namespace MultipleChoise.Server.Controllers
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
        private readonly IQuizRepository _quizRepository;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IQuizRepository quizRepository)
        {
            _logger = logger;
            _quizRepository = quizRepository;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
        public class CreateQuizRequest
        {
            public string Title { get; set; }
            public TimeSpan Duration { get; set; }
        }


        [HttpPost]
        public async Task<IActionResult> CreateQuizAsync([FromBody] CreateQuizRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid quiz data.");
            }

            var quiz = new Quiz
            {
                Title = request.Title,
                Duration = new TimeSpan(2,3,1)
            };

            try
            {
                //var t = DateTime.Now.
                var createdQuiz = await _quizRepository.CreateQuizAsync(quiz);
                return CreatedAtAction(nameof(CreateQuizAsync), new { id = createdQuiz.Id }, createdQuiz);
            }
            catch (Exception ex)
            {

            }
            return BadRequest("errr");

        }
    }
}
