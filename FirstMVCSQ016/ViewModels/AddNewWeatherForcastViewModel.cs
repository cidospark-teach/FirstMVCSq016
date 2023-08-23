using System.ComponentModel.DataAnnotations;

namespace FirstMVCSQ016.ViewModels
{
    public class AddNewWeatherForcastViewModel
    {
        [Required]
        public int TemperatureC { get; set; }

        [Required]
        public string? Summary { get; set; }
    }
}
