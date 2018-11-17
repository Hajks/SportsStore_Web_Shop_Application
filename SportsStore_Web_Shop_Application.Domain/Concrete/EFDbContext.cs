using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsStore_Web_Shop_Application.Domain.Entities;

namespace SportsStore_Web_Shop_Application.Domain.Concrete
{
     public class EFDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
    }
}
