using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ecomindo_D1.Model
{
    public class Menu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid idMenu { get; set; }
        //public Restaurant Restaurant { get; set; }
        //public Guid idRestaurant { get; set; }
        public string namaMenu { get; set; }
        public int hargaMenu { get; set; }
    }
}
