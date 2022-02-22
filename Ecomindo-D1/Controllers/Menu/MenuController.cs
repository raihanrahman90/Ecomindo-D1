using Ecomindo_D1.Controllers.Menu.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecomindo_D1.Controllers.Menu
{
    public class MenuController : Controller
    {
        private readonly ILogger<MenuController> _logger;

        public MenuController(ILogger<MenuController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        [ProducesResponseType(typeof(MenuDTO), 200)]
        [ProducesResponseType(typeof(string), 400)]
        public async Task<MenuDTO> InsertOne()
        {
            var result = new MenuDTO
            {
                idMenu=1,
                namaMenu="Bakso",
                hargaMenu=15000
            };
            return result;
        }
    }
}
