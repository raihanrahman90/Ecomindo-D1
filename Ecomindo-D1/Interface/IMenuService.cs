using Ecomindo_D1.DTO;
using System;
using System.Threading.Tasks;

namespace Ecomindo_D1.Interface
{
    public interface IMenuService
    {
        Task<ListMenuDTO> getAll();
        Task<bool> insert(string nama, int harga, Guid idRestaurant);
        Task<bool> update(Guid id, MenuDTO menuDTO);
        Task<MenuDTO> GetOne(Guid id);
        Task<bool> deleteOne(Guid id);
    }
}
