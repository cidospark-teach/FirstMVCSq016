using FirstMVCSQ016.Models;
using FirstMVCSQ016.Services;
using FirstMVCSQ016.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FirstMVCSQ016.Controllers
{
    public class WeatherForcastController : Controller
    {
        private readonly IWeatherForecastService _weatherForecastService;
        public WeatherForcastController(IWeatherForecastService weatherForecastService)
        {
            _weatherForecastService = weatherForecastService;
        }

        [Authorize(Roles = "admin, editor")]  // you must be logged in, you musst be an editor or an admin
        [Authorize(Policy = "CanAdd")]
        public async Task<IActionResult> AddWeatherForcast(AddNewWeatherForcastViewModel model)
        {
            if(ModelState.IsValid)
            {
                var result = await _weatherForecastService.AddNewAsync(model);

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }
    }
}
