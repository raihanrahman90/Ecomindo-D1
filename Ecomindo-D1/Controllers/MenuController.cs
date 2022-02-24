using AutoMapper;
using Ecomindo_D1.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using Ecomindo_D1.DTO;
using Ecomindo_D1.Interface;

namespace Ecomindo_D1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MenuController : Controller
    {
        private readonly ILogger<MenuController> _logger;
        private IMenuService _menuService;
        public MenuController(ILogger<MenuController> logger, IMenuService menuService)
        {
            _logger = logger;
            _menuService = menuService;
        }
        [HttpGet]
        [ProducesResponseType(typeof(ListMenuDTO), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<ListMenuDTO> GetAllMenu()
        {
            var result = await _menuService.getAll();
            return result;
        }
        [HttpPost]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<ActionResult> InsertMenu([FromBody] MenuDTO menuDTO)
        {
            try
            {
                await _menuService.insert(menuDTO.namaMenu, menuDTO.hargaMenu, menuDTO.idRestaurant);
                return new OkResult();
            }
            catch (Exception)
            {
                return new BadRequestResult();
            }
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(MenuDTO), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<MenuDTO> GetOne([FromRoute] Guid id)
        {
            try
            {
                var hasil = await _menuService.GetOne(id);
                return hasil;
            }
            catch (Exception)   
            {
                return null;
            }
        }
        [HttpPost]
        [Route("{id}")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<ActionResult> UpdateMenu([FromRoute] Guid id, [FromBody] MenuDTO menuDTO)
        {
            try
            {
                await _menuService.update(id, menuDTO);
                return new OkResult();
            }
            catch (Exception)
            {
                return new BadRequestResult();
            }
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<ActionResult> DeleteMenu([FromRoute] Guid id)
        {
            try
            {
                await _menuService.deleteOne(id);
                return new OkResult();
            }
            catch (Exception)
            {
                return new BadRequestResult();
            }
        }
    }
}
