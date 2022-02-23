using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecomindo_D1.DTO
{
    public class RestaurantDTO
    {
        public string namaRestaurant { get; set; }
    }
    public class ListRestaurantDTO
    {
        public List<RestaurantDTO> listRestaurant { get; set; }
    }
}
