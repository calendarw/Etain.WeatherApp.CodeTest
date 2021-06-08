using Etain.WeatherApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Etain.WeatherApp.Data
{
    public class WeatherForecastRepository : IWeatherForecastRepository
    {
        private readonly IHttpClientFactory _clientFactory;
        private List<WeatherForecast> _list = new List<WeatherForecast>();
        public WeatherForecastRepository(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            Task.Run(() => this.ReloadAsync()).Wait();
        }

        public List<WeatherForecast> GetAll(){
            return _list.ToList();
        }
        public async Task ReloadAsync(){

            HttpClient client = _clientFactory.CreateClient();

            using (var request = new HttpRequestMessage(new HttpMethod("GET"), "https://www.metaweather.com/api/location/44544/"))
            {
                var response = await client.SendAsync(request);
                using (HttpContent content = response.Content)
                {
                    var jsonString = await content.ReadAsStringAsync();
                    WeatherReport data = Newtonsoft.Json.JsonConvert.DeserializeObject<WeatherReport>(jsonString);
                    _list.Clear();
                    _list.AddRange(data.consolidated_weather);
                }
            }
        }
    }
}