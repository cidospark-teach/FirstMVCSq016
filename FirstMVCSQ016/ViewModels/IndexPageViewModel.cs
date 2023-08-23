namespace FirstMVCSQ016.ViewModels
{
    public class IndexPageViewModel
    {
        public string SearchTerm { get; set; } = "";
        public IEnumerable<WeatherForecastViewModel> WeatherResults { get; set; } = new List<WeatherForecastViewModel>();
    }
}
