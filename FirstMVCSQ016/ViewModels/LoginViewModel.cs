using System.ComponentModel.DataAnnotations;

namespace FirstMVCSQ016.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Email { get; set; }= string.Empty;

        [Required(ErrorMessage ="Password is required!")]
        public string Password { get; set; } = string.Empty;
    }
}
