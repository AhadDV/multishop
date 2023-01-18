using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MultiShopBB202.Dtos.AppUserDtos
{
    public class LoginDto
    {
        public string UserName { get; set; }

        [PasswordPropertyText]
        public string Password { get; set; }
    }
}
