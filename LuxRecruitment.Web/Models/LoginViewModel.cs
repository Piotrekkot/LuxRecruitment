using System.ComponentModel.DataAnnotations;

namespace LuxRecruitment.Web.Models
{
    public sealed class LoginViewModel
    {
        [Required(ErrorMessage = "Proszę podać nazwę użytkownika.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Proszę podać hasło.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
