using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecomindo_D1.DTO
{
    public class MenuDTO
    {
        public Guid idMenu { get; set; }
        public string namaMenu { get; set; }
        public int hargaMenu { get; set; }
        public Guid idRestaurant { get; set; }
    }
    public class ListMenuDTO
    {
        public List<MenuDTO> listMenu { get; set; }
    }
}
