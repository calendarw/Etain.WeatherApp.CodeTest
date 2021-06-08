using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Etain.WeatherApp.Data;
using Etain.WeatherApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Etain.WeatherApp.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherForecastRepository _repository;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return _repository.GetAll().Where(r => r.applicable_date < DateTime.Today.AddDays(5));
        }
    }
}
