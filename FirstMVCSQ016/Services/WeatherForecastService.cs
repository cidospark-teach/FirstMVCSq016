using FirstMVCSQ016.Models;
using FirstMVCSQ016.Models.Enums;
using FirstMVCSQ016.ViewModels;

namespace FirstMVCSQ016.Services
{
    public class WeatherForecastService : BaseService, IWeatherForecastService
    {
        public WeatherForecastService(HttpClient client, IConfiguration config): base(client, config)
        {
        }

        public async Task<IEnumerable<WeatherForecast>> GetAll()
        {
            var address = "/WeatherForecast/get-all";
            var methodType = ApiVerbs.GET.ToString();

            var result = await MakeRequest<IEnumerable<WeatherForecast>, string>(address, methodType, "", "");

            if (result != null)
                return result;

            return new List<WeatherForecast>();
        }

        public async Task<bool> AddNewAsync(AddNewWeatherForcastViewModel entity)
        {
            var address = "/WeatherForecast/add";
            var methodType = ApiVerbs.POST.ToString();

            //var authResult = await MakeRequest<string, WeatherForecast>(address, methodType, entity, "");

            var result = await MakeRequest<ResponseObject<string, string>, AddNewWeatherForcastViewModel>(address, methodType, entity, "");

            if (result != null)
                return true;

            return false;
        }
    }
}
