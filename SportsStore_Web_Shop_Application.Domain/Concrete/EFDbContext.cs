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
        public EFDbContext() : base()
        {
            Database.SetInitializer(new DataBaseInitializer());
        }

        public DbSet<Product> Products { get; set; }
    }

    public class DataBaseInitializer : DropCreateDatabaseIfModelChanges<EFDbContext>
    {
        protected override void Seed(EFDbContext db)
        {
            var product1 = new Product() { Name = "Kajak", Description = "Łodka przeznaczona dla jednej osoby", Category = "Sporty wodne", Price = 275.00M, ProductID = 1};
            var product2 = new Product() { Name = "Kamizelka ratunkowa", Description = "Chroni i dodaje uroku", Category = "Sporty wodne", Price = 48.95M, ProductID = 2 };
            var product3 = new Product() { Name = "Piłka", Description = "Zatwierdzona przez FIFA rozmiar i waga", Category = "Piłka nożna", Price = 19.50M, ProductID = 3 };
            var product4 = new Product() { Name = "Flagi narożne", Description = "Nadadzą twojemu boisku profesjonalny wygląd", Category = "Piłka nożna", Price = 34.95M, ProductID = 4 };
            var product5 = new Product() { Name = "Stadion", Description = "Składany stadion na 35 000 osób", Category = "Piłka nożna", Price = 79500.00M, ProductID = 5 };
            var product6 = new Product() { Name = "Czapka", Description = "Zwiększa efektywność mózgu o 75%", Category = "Szachy", Price = 16.00M, ProductID = 6 };
            var product7 = new Product() { Name = "Niestabilne krzesło", Description = "Zmniejsza szanse przeciwnika", Category = "Szachy", Price = 29.95M, ProductID = 7 };
            var product8 = new Product() { Name = "Ludzka szachownica", Description = "Przyjemna gra dla całej rodziny!", Category = "Szachy", Price = 75.00M, ProductID = 8 };
            var product9 = new Product() { Name = "Błyszczący król", Description = "Figura pokryta złotem i wysadzana diamentami", Category = "Szachy", Price = 1200M, ProductID = 9 };

            db.Products.Add(product1);
            db.Products.Add(product2);
            db.Products.Add(product3);
            db.Products.Add(product4);
            db.Products.Add(product5);
            db.Products.Add(product6);
            db.Products.Add(product7);
            db.Products.Add(product8);
            db.Products.Add(product9);

            base.Seed(db);
        }
    }
}