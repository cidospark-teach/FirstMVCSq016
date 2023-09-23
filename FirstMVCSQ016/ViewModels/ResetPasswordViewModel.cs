using System.ComponentModel.DataAnnotations;

namespace FirstMVCSQ016.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required]
        public string Email { get; set; }= string.Empty;


        [Required]
        public string Token { get; set; } = string.Empty;


        [Required(ErrorMessage ="Password is required!")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;


        [Required]
        [Compare("Password", ErrorMessage ="Passsword mismatch")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = string.Empty;

    }
}
