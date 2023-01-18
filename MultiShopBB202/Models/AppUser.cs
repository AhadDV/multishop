using Microsoft.AspNetCore.Identity;

namespace MultiShopBB202.Models
{
    public class AppUser:IdentityUser
    {
        public string FullName { get; set; }
    }
}
