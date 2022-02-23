using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecomindo_D1.Model
{
    public class OnBoardingSkdDbContext : DbContext
    {
        public OnBoardingSkdDbContext(DbContextOptions<OnBoardingSkdDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {

        }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
    }
}
