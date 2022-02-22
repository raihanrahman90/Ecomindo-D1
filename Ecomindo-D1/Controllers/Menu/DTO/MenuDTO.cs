using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecomindo_D1.Controllers.Menu.DTO
{
    public class MenuDTO
    {
        public int idMenu { get; set; }
        public string namaMenu { get; set; }
        public int hargaMenu { get; set; }
    }
    public class ListMenuDTO
    {
        public List<MenuDTO> listMenu { get; set; }
    }
}
