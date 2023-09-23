using System.ComponentModel.DataAnnotations;

namespace FirstMVCSQ016.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        public string Email { get; set; }= string.Empty;
    }
}
