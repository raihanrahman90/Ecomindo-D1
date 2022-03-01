using Ecomindo_D1.DTO;
using Ecomindo_D1.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecomindo_D1.Interface
{
    public interface IRestaurantService
    {
        Task<List<RestaurantWithMenusDTO>> getAll();
        Task<RestaurantWithMenusDTO> GetOne(Guid id);
        Task<bool> insert(RestaurantDTO restaurant);
    }
}
