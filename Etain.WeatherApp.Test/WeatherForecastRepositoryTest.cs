using Etain.WeatherApp.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Etain.WeatherApp.Data.Test
{
    [TestClass]
    public class WeatherForecastRepositoryTest
    {
        WeatherApp.Data.WeatherForecastRepository repos;

        private IHttpClientFactory CreateHttpClientFactory()
        {
            var mockFactory = new Mock<IHttpClientFactory>();
            var client = new HttpClient();
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
            return mockFactory.Object;
        }

        [TestMethod]
        public async Task ProofOfConcept()
        {
            HttpClient client = CreateHttpClientFactory().CreateClient();

            using (var request = new HttpRequestMessage(new HttpMethod("GET"), "https://www.metaweather.com/api/location/44544/"))
            {
                var response = await client.SendAsync(request);
                using (HttpContent content = response.Content)
                {
                    var jsonString = await content.ReadAsStringAsync();
                    WeatherReport data = Newtonsoft.Json.JsonConvert.DeserializeObject<WeatherReport>(jsonString);
                    Assert.IsTrue(data.title == "Belfast");
                    Assert.IsTrue(data.consolidated_weather.Count > 0);
                }
            }
        }

        [TestMethod]
        public void TestGetAll()
        {
            repos = new WeatherForecastRepository(CreateHttpClientFactory());
            Assert.IsTrue(repos.GetAll().Count > 0);
        }
    }
}
