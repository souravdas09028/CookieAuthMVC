using System.ComponentModel.DataAnnotations;

namespace CookieAuthMVC.Models.Request
{
    public class LogInRequest
    {
        [Required]
        public string UserID { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
