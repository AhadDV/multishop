using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MultiShopBB202.Dtos.AppUserDtos;
using MultiShopBB202.Models;
using System.Text.RegularExpressions;

namespace MultiShopBB202.Controllers
{
    public class AccountController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager,SignInManager<AppUser> signInManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        //public async Task<IActionResult> CreateRole()
        //{
        //    await _roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
        //    await _roleManager.CreateAsync(new IdentityRole { Name = "SuperAdmin" });
        //    await _roleManager.CreateAsync(new IdentityRole { Name = "User" });
        //    return Json("Okey");
        //}


        public async Task<IActionResult> Register()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if (!ModelState.IsValid)
                return View();


            AppUser appUser = new AppUser
            {
                Email = dto.Email,
                UserName = dto.UserName,
                FullName = dto.FullName,
                
            };

            IdentityResult result = await _userManager.CreateAsync(appUser, dto.Password);

            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                    return View();
                }
            }



            await _userManager.AddToRoleAsync(appUser, "User");

            return RedirectToAction("index", "home");
        }

        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            AppUser appUser=await _userManager.FindByNameAsync(loginDto.UserName);

            if(appUser == null)
            {
                ModelState.AddModelError("", "UserName or Password incorrect");
                return View();
            }

            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(appUser,loginDto.Password,true,true);

            if (!result.Succeeded) {
            
                if(result.IsLockedOut)
                {
                    
                    ModelState.AddModelError("", "your account blocekd for 5 minutes");
                    return View();
                }
         
                    ModelState.AddModelError("", "UserName or Password incorrect");
                    return View();
            }


                


            return RedirectToAction("index", "home");
        }

        [Authorize]
        public async Task<IActionResult> MyProfile()
        {
            AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);

            UserGetDto userGetDto = new UserGetDto
            {
                Email= appUser.Email,
                FullName= appUser.FullName,
                UserName= appUser.UserName,
            };


            return View(userGetDto);
        }

        public async Task<IActionResult> Logout()
        {

            await _signInManager.SignOutAsync();
            return RedirectToAction("index","home");
        }
    }
}
