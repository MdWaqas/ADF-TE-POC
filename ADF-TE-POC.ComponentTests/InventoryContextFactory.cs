using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADF_TE_POC.DTOs;
using ADF_TE_POC.Models;
using FizzWare.NBuilder;
using Microsoft.EntityFrameworkCore;

namespace ADF_TE_POC.ComponentTests
{
    public static class InventoryContextFactory
    {
        public static InventoryContext Create()
        {
            var options = new DbContextOptionsBuilder<InventoryContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var context = new InventoryContext(options);
            context.Database.EnsureCreated();
            SeedSampleData(context);
            return context;
        }

        public static void SeedSampleData(InventoryContext context)
        {
            context.Users.AddRange(new User
                {
                    Id = 0,
                    FirstName = "Waqas",
                    LastName = "Idrees",
                    Email = "waqas.idrees@confiz.com",
                    Password = "123456",
                    CreatedDate = DateTime.Now
                },
                new User
                {
                    Id = 0,
                    FirstName = "Service",
                    LastName = "Account",
                    Email = "sa@confiz.com",
                    Password = "123456",
                    CreatedDate = DateTime.Now
                });

            IEnumerable<Product> products = Builder<Product>.CreateListOfSize(10).All()
                .With(t => t.Name = Faker.Lorem.GetFirstWord())
                .With(t=>t.Color = Faker.Lorem.GetFirstWord())
                .With(t=>t.Category=Faker.Lorem.GetFirstWord())
                .With(t=>t.UnitPrice = Faker.RandomNumber.Next(1,1000))
                .With(t=>t.AvailableQuantity = Faker.RandomNumber.Next(1,150))
                .Build();
            context.Products.AddRange(products);

            context.SaveChanges();

        }

        public static void Destroy(InventoryContext context)
        {
            context.Database.EnsureDeleted();

            context.Dispose();
        }
    }
}
