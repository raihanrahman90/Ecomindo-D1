using AutoMapper;
using Ecomindo_D1.bll;
using Ecomindo_D1.Model;
using Ecomindo_D1.Controllers.Menu.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecomindo_D1.Controllers.Menu
{
    [ApiController]
    [Route("[controller]")]
    public class MenuController : Controller
    {
        private readonly ILogger<MenuController> _logger;
        private readonly MenuService menuService;
        private readonly IMapper _mapper;
        public MenuController(ILogger<MenuController> logger )
        {
            this.menuService = new MenuService();
            MapperConfiguration config = new MapperConfiguration(m =>
            {
                m.CreateMap<MenuDTO, Ecomindo_D1.Model.Menu>();
                m.CreateMap<Ecomindo_D1.Model.Menu, MenuDTO>();
            });
            _logger = logger;
        }
        [HttpGet]
        [ProducesResponseType(typeof(ListMenuDTO), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<ListMenuDTO> GetAllMenu()
        {
            var allMenu = this.menuService.getAll().Select(x=>new MenuDTO { idMenu=x.idMenu,namaMenu=x.namaMenu,hargaMenu=x.hargaMenu}).ToList();
            var result = new ListMenuDTO
            {
                listMenu = allMenu
            };
            return result;
        }
        [HttpPost]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<ActionResult> InsertMenu([FromBody] MenuDTO menuDTO)
        {
            try
            {
                var hasil = this.menuService.insert(menuDTO.namaMenu, menuDTO.hargaMenu);
                if (hasil != true) return new BadRequestResult();
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
        public async Task<MenuDTO> GetOne([FromRoute] int id)
        {
            try
            {
                var hasil = this.menuService.getOne(id);
                return new MenuDTO { idMenu=hasil.idMenu, namaMenu=hasil.namaMenu, hargaMenu=hasil.hargaMenu };
            }
            catch (Exception)   
            {
                return null;
            }
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<ActionResult> DeleteMenu([FromRoute] int id)
        {
            try
            {
                var delete = this.menuService.deleteOne(id);
                if (delete) return new OkResult();
                return new BadRequestResult();
            }
            catch (Exception)
            {
                return new BadRequestResult();
            }
        }
    }
}
