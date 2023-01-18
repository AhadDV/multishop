using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MultiShopBB202.Dtos.AppUserDtos
{
    public class RegisterDto
    {
        public string FullName { get; set; }
        public string UserName { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        [PasswordPropertyText]
        public string Password { get; set; }
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
        [Required]
        public bool Terms { get; set; }
    }
}
