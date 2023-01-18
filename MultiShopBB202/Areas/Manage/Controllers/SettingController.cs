using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShopBB202.Context;
using MultiShopBB202.Dtos.SettingDtos;
using MultiShopBB202.Models;

namespace MultiShopBB202.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class SettingController : Controller
    {
        private readonly MultiDbContext _context;

        public SettingController(MultiDbContext context)
        {
            _context = context;
        }



        public IActionResult Index()
        {

            Setting? setting = _context.Settings.FirstOrDefault();
            SettingGetDto settingGetDto = new SettingGetDto { Adress = setting.Adress, Email = setting.Email, PhoneNumber = setting.PhoneNumber };
            return View(settingGetDto);
        }

        public IActionResult Update()
        {
            Setting? setting = _context.Settings.FirstOrDefault();
            SettingUpdateDto SettingPostDto = new SettingUpdateDto
            {
                settingGetDto = new SettingGetDto { Adress = setting.Adress, Email = setting.Email, PhoneNumber = setting.PhoneNumber }
            };
            return View(SettingPostDto);
        }

        [HttpPost]
        public IActionResult Update(SettingUpdateDto SettingUpdateDto)
        {
            Setting setting = _context.Settings.FirstOrDefault();
            setting.PhoneNumber = SettingUpdateDto.settingPostDto.PhoneNumber;
            setting.Adress = SettingUpdateDto.settingPostDto.Adress;
            setting.Email = SettingUpdateDto.settingPostDto.Email;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}
