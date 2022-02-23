using AutoMapper;
using Ecomindo_D1.bll;
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

namespace Ecomindo_D1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MenuController : Controller
    {
        private readonly ILogger<MenuController> _logger;
        private UnitOfWork unitOfWork;
        private IMapper _mapper;
        public MenuController(ILogger<MenuController> logger, UnitOfWork unitOfWork, IMapper mapper )
        {
            this.unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(typeof(ListMenuDTO), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<ListMenuDTO> GetAllMenu()
        {
            var allMenu = this.unitOfWork.MenuRepository.GetAll();
            var hasil = await allMenu.ProjectTo<MenuDTO>(_mapper.ConfigurationProvider).ToListAsync();
            var result = new ListMenuDTO
            {
                listMenu = hasil
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
                var menu = _mapper.Map<Menu>(menuDTO);
                await unitOfWork.MenuRepository.AddAsync(menu);
                await unitOfWork.SaveAsync();
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
                var menu= await unitOfWork.MenuRepository.GetByIdAsync(id);
                var hasil = _mapper.Map<MenuDTO>(menu);
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
                var menu = await unitOfWork.MenuRepository.GetByIdAsync(id);
                menu.namaMenu = menuDTO.namaMenu;
                menu.hargaMenu = menuDTO.hargaMenu;
                this.unitOfWork.MenuRepository.Edit(menu);
                await this.unitOfWork.SaveAsync();
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
                var menu = await this.unitOfWork.MenuRepository.GetByIdAsync(id);
                this.unitOfWork.MenuRepository.Delete(menu);
                await this.unitOfWork.SaveAsync();
                return new OkResult();
            }
            catch (Exception)
            {
                return new BadRequestResult();
            }
        }
    }
}
