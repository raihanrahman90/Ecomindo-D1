using AutoMapper;
using AutoMapper.QueryableExtensions;
using Ecomindo_D1.DTO;
using Ecomindo_D1.Interface;
using Ecomindo_D1.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecomindo_D1.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly ILogger<RestaurantService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRedisService _redisService;

        public RestaurantService(ILogger<RestaurantService> logger, IUnitOfWork unitOfWork, IMapper mapper, IRedisService redisService)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _redisService = redisService;
        }

        public async Task<List<RestaurantWithMenusDTO>> getAll()
        {

            var result = await _unitOfWork.RestaurantRepository.GetAll().ProjectTo<RestaurantWithMenusDTO>(_mapper.ConfigurationProvider).ToListAsync();
            return result;
        }
        public async Task<RestaurantWithMenusDTO> GetOne(Guid id)
        {
            try
            {
                var result = await _redisService.GetAsync<RestaurantWithMenusDTO>($"restaurant_restaurantID:{id}");
                if (result == null)
                {
                    result = await _unitOfWork.RestaurantRepository.GetAll()
                        .Where(b => b.idRestaurant == id)
                        .ProjectTo<RestaurantWithMenusDTO>(_mapper.ConfigurationProvider)
                        .FirstOrDefaultAsync();
                    await _redisService.SaveAsync($"restaurant_restaurantID:{id}", result);
                }
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> insert(RestaurantDTO restaurantDTO)
        {
            var restaurant = _mapper.Map<Restaurant>(restaurantDTO);
            await _unitOfWork.RestaurantRepository.AddAsync(restaurant);
            await _unitOfWork.SaveAsync();
            return true;
        }
    }
}
