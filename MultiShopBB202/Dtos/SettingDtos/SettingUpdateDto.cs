namespace MultiShopBB202.Dtos.SettingDtos
{
    public class SettingUpdateDto
    {
        public SettingPostDto settingPostDto { get; set; }
        public SettingGetDto settingGetDto { get; set; }

        public SettingUpdateDto()
        {
            settingPostDto = new SettingPostDto();
        }
    }
}
