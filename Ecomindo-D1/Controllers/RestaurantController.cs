using AutoMapper;
using AutoMapper.QueryableExtensions;
using Ecomindo_D1.DTO;
using Ecomindo_D1.Interface;
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

        private readonly ILogger<RestaurantController> _logger;
        private readonly IRestaurantService _restaurantService;
        public RestaurantController(ILogger<RestaurantController> logger, IRestaurantService restaurantService) 
        {
            _logger = logger;
            _restaurantService = restaurantService;
        }
        [HttpGet]
        [ProducesResponseType(typeof(ListRestaurantDTO), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<ListRestaurantDTO> getAll()
        {
            var hasil = await _restaurantService.getAll();
            return hasil;
        }


        [HttpPost]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<IActionResult> add([FromBody] RestaurantDTO restaurantDTO)
        {
            var result = await _restaurantService.insert(restaurantDTO);
            return new OkResult();
        }
    }
}
