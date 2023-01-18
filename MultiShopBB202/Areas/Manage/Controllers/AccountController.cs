using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MultiShopBB202.Dtos.AppUserDtos;
using MultiShopBB202.Models;

namespace MultiShopBB202.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _siginManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> siginManager)
        {
            _userManager = userManager;
            _siginManager = siginManager;
        }

        //public async Task<IActionResult> Index()
        //{
        //    AppUser appUser = new AppUser { UserName = "Admin", FullName = "Seyhun Ceferov" };
        //    await _userManager.CreateAsync(appUser, "Admin123@");

        //    await _userManager.AddToRoleAsync(appUser,"Admin");
        //    return Json("Ok");
        //}


        public async Task<IActionResult> Login()
        {
            return View();
        }

            [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            AppUser appUser = await _userManager.FindByNameAsync(loginDto.UserName);

            if (appUser == null)
            {
                ModelState.AddModelError("", "UserName or password incorrect");
                return View();
            }

            bool result = await _userManager.IsInRoleAsync(appUser, "Admin");

            if (!result)
            {
                ModelState.AddModelError("", "UserName or password incorrect");
                return View();
            }

            Microsoft.AspNetCore.Identity.SignInResult signInResult =  await _siginManager.PasswordSignInAsync(appUser,loginDto.Password,true,false);
                   

            if(!signInResult.Succeeded) {
                ModelState.AddModelError("", "UserName or password incorrect");
                return View();
            }

            return RedirectToAction("Index","Setting");
        }

    }
}
