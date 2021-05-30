using System;
using System.Collections.Generic;
using ADF_TE_POC.Models;
using Microsoft.EntityFrameworkCore;

namespace ADF_TE_POC.IntegrationTests
{
    public static class InventoryContextFactory
    {
        public static InventoryContext Create()
        {
            var options = new DbContextOptionsBuilder<InventoryContext>().UseSqlServer("Data Source=CONFIZ-4418;Initial Catalog=Adf-Poc;Persist Security Info=True;User ID=sa;Password=123;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;").Options;
            var context = new InventoryContext(options);
            return context;
        }

        public static void Destroy(InventoryContext context)
        {
            context.Dispose();
        }
    }
}
