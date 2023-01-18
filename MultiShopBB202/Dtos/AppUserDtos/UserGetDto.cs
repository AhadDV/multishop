using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MultiShopBB202.Dtos.AppUserDtos
{
    public class UserGetDto
    {
        public string FullName { get; set; }
        public string UserName { get; set; }

        [EmailAddress]
        public string Email { get; set; }
 
    }
}
