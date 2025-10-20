namespace Goreu.Services.Profiles
{
    public class MenuRolProfile:Profile
    {
        public MenuRolProfile()
        {
            CreateMap<MenuRolRequestDto, MenuRol>();
            CreateMap<MenuRolInfo, MenuRol>();
            CreateMap<MenuRol, MenuRolInfo>();
            CreateMap<MenuRol,MenuRolResponseDto>();
        }
    }
}
