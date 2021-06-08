using Etain.WeatherApp.Models;
using System.Threading.Tasks;

namespace Etain.WeatherApp.Data
{
    public interface IWeatherForecastRepository : IReadOnlyRepository<WeatherForecast, long>
    {
        Task ReloadAsync();
    }
}
