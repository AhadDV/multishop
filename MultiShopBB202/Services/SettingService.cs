using MultiShopBB202.Context;
using MultiShopBB202.Dtos.SettingDtos;
using MultiShopBB202.Models;

namespace MultiShopBB202.Services
{
    public class SettingService : ISettingService
    {
        private readonly MultiDbContext _context;

        public SettingService(MultiDbContext context)
        {
            _context = context;
        }

        public SettingGetDto GetSetting()
        {
            Setting setting = _context.Settings.FirstOrDefault();

            SettingGetDto settingGetDto = new SettingGetDto
            {
                Adress = setting.Adress,
                Email = setting.Email,
                PhoneNumber = setting.PhoneNumber,
            };
            return settingGetDto;
        }
            
      
    }
}
