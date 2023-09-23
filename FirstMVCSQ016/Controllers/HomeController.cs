using FirstMVCSQ016.Models;
using FirstMVCSQ016.Services;
using FirstMVCSQ016.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FirstMVCSQ016.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWeatherForecastService _weatherforecastService;
        private readonly IUploadService _uploadService;

        public HomeController(ILogger<HomeController> logger, IWeatherForecastService weatherForecastService, IUploadService uploadService)
        {
            _logger = logger;
            _weatherforecastService= weatherForecastService;
            _uploadService = uploadService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(IndexPageViewModel model)
        {
            var result = await _weatherforecastService.GetAll();
            var mappedResult = new List<WeatherForecast>();

            mappedResult = result.ToList();

            if (model.SearchTerm != null && model.SearchTerm != "")
            {
                var newResult = result.Where(x => x.Summary == model.SearchTerm);
                mappedResult = newResult.ToList();
            }

            var viewResult = mappedResult.Select(x => new WeatherForecastViewModel
            {
                Id = x.Id,
                Photo = x.Summary == "testing"? $"~/images/cloudy.jpg"  : $"~/images/{x.Summary}.jpg",
                TemperatureC = x.TemperatureC,
                TemperatureF = x.TemperatureF,
                Summary= x.Summary,
            });

            var pageModel = new IndexPageViewModel();
            pageModel.WeatherResults = viewResult;

            return View(pageModel);
        }

        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult UploadImage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(UploadImageViewModel model)
        {
            var result = await _uploadService.UploadImage(model.Photo, model.FolderName);
            if(result.ContainsKey("Code") && result["Code"].Equals("200"))
            {
                ViewBag.Success = $"PublicId: {result["PublicId"]}, Url: {result["Url"]}";
                return View();
            }


            ViewBag.Err = $"Error: {result["Message"]}";
            return View();
        }
    }
}