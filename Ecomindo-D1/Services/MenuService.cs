using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ecomindo_D1.Model;
using System.Linq;
using Microsoft.Extensions.Logging;
using Ecomindo_D1.DTO;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Ecomindo_D1.Interface;

namespace Ecomindo_D1.Services
{
    public class MenuService : IMenuService
    {
        private readonly ILogger<MenuService> _logger;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private IRedisService _redisService;
        public MenuService(ILogger<MenuService> logger, IUnitOfWork unitOfWork, IMapper mapper, IRedisService redisService)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _redisService = redisService;
        }
        public async Task<ListMenuDTO> getAll()
        {
            var allMenu = _unitOfWork.MenuRepository.GetAll();
            var hasil = await allMenu.ProjectTo<MenuDTO>(_mapper.ConfigurationProvider).ToListAsync();
            var result = new ListMenuDTO
            {
                listMenu = hasil
            };
            return result;
        }
        public async Task<bool> insert(string nama, int harga, Guid idRestaurant)
        {
            try
            {
                await _unitOfWork.MenuRepository.AddAsync(new Menu { namaMenu = nama, hargaMenu = harga, idRestaurant=idRestaurant });
                await _unitOfWork.SaveAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<MenuDTO> GetOne(Guid id)
        {
            try
            {
                var result = await _redisService.GetAsync<MenuDTO>($"menu_menuID:{id}");
                if (result == null)
                {
                    var menu = await _unitOfWork.MenuRepository.GetByIdAsync(id);
                    result = _mapper.Map<MenuDTO>(menu);
                    await _redisService.SaveAsync($"menu_menuID:{id}", result);
                }

                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public async Task<bool> deleteOne(Guid id)
        {
            try
            {
                var menu = await _unitOfWork.MenuRepository.GetByIdAsync(id);
                _unitOfWork.MenuRepository.Delete(menu);
                await _unitOfWork.SaveAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> update(Guid id, MenuDTO menuDTO)
        {
            try
            {
                var menu = await _unitOfWork.MenuRepository.GetByIdAsync(id);
                menu.hargaMenu = menuDTO.hargaMenu;
                menu.namaMenu = menuDTO.namaMenu;
                menu.idRestaurant = menuDTO.idRestaurant;
                _unitOfWork.MenuRepository.Edit(menu);
                await _unitOfWork.SaveAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

}
