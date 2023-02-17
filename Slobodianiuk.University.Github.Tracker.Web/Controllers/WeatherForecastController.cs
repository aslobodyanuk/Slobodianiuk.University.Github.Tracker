using Microsoft.AspNetCore.Mvc;

namespace Slobodianiuk.University.Github.Tracker.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        public WeatherForecastController()
        {
        }

        //[HttpGet]
        //[Route("Get")]
        //public IEnumerable<WeatherForecast> Get()
        //{
        //    return _forecastService.GetRandomForecast();
        //}
    }
}