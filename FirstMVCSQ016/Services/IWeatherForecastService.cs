using FirstMVCSQ016.Models;
using FirstMVCSQ016.ViewModels;

namespace FirstMVCSQ016.Services
{
    public interface IWeatherForecastService
    {
        Task<IEnumerable<WeatherForecast>> GetAll();
        Task<bool> AddNewAsync(AddNewWeatherForcastViewModel entity);
    }
}
