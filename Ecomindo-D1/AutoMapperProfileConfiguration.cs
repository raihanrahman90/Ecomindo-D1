using AutoMapper;
using Ecomindo_D1.DTO;
using Ecomindo_D1.Model;

namespace Ecomindo_D1
{
    public class AutoMapperProfileConfiguration : Profile
    {
        public AutoMapperProfileConfiguration()
        {
            CreateMap<MenuDTO, Menu>();
            CreateMap<Menu, MenuDTO>();
            CreateMap<RestaurantDTO, Restaurant>();
            CreateMap<Restaurant, RestaurantDTO>();
            CreateMap<RestaurantWithMenusDTO, Restaurant>();
            CreateMap<Restaurant, RestaurantWithMenusDTO>();
        }
    }
}
