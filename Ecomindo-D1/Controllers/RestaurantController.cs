using AutoMapper;
using AutoMapper.QueryableExtensions;
using Ecomindo_D1.DTO;
using Ecomindo_D1.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecomindo_D1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RestaurantController : Controller
    {

        private UnitOfWork _unitOfWork;
        private readonly ILogger<RestaurantController> _logger;
        private IMapper _mapper;
        public RestaurantController(ILogger<RestaurantController> logger, UnitOfWork unitOfWork, IMapper mapper) 
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(typeof(ListRestaurantDTO), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<ListRestaurantDTO> getAll()
        {
            var result = await _unitOfWork.RestaurantRepository.GetAll().ProjectTo<RestaurantDTO>(_mapper.ConfigurationProvider).ToListAsync();
            var hasil = new ListRestaurantDTO { listRestaurant = result };
            return hasil;
        }


        [HttpPost]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> add([FromBody] RestaurantDTO restaurantDTO)
        {
            var restaurant = _mapper.Map<Restaurant>(restaurantDTO);
            if (restaurant == null) Console.WriteLine("iini null");
            await _unitOfWork.RestaurantRepository.AddAsync(restaurant);
            await _unitOfWork.SaveAsync();
            return new OkResult();
        }
    }
}
