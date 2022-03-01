using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecomindo_D1.DTO
{
    public class RestaurantDTO
    {
        public Guid idRestaurant { get; set; }
        public string namaRestaurant { get; set; }
    }
    public class ListRestaurantDTO
    {
        public List<RestaurantDTO> listRestaurant { get; set; }
    }
    public class RestaurantWithMenusDTO : RestaurantDTO
    {
        public List<MenuDTO> Menus { get; set; }
    }
}
