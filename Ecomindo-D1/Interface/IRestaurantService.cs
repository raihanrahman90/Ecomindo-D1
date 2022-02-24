using Ecomindo_D1.DTO;
using Ecomindo_D1.Model;
using System.Threading.Tasks;

namespace Ecomindo_D1.Interface
{
    public interface IRestaurantService
    {
        Task<ListRestaurantDTO> getAll();
        Task<bool> insert(RestaurantDTO restaurant);
    }
}
