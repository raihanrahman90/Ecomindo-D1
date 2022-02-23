using Ecomindo_D1.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecomindo_D1.Repository
{
    public class MenuRepository : BaseRepository<Menu>
    {
        public MenuRepository(DbContext dbContext) : base(dbContext) { }
    }
}
