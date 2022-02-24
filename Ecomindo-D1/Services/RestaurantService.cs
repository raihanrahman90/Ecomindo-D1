using AutoMapper;
using AutoMapper.QueryableExtensions;
using Ecomindo_D1.DTO;
using Ecomindo_D1.Interface;
using Ecomindo_D1.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Ecomindo_D1.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly ILogger<RestaurantService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RestaurantService(ILogger<RestaurantService> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ListRestaurantDTO> getAll()
        {
            var result = await _unitOfWork.RestaurantRepository.GetAll().ProjectTo<RestaurantDTO>(_mapper.ConfigurationProvider).ToListAsync();
            var hasil = new ListRestaurantDTO { listRestaurant = result };
            return hasil;
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
