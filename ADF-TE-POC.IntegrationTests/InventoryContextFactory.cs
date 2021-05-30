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
            var options = new DbContextOptionsBuilder<InventoryContext>().UseSqlServer("Data Source=tcp:nuhealthpartners-dev.database.windows.net,1433;Initial Catalog=Adf-Poc;Persist Security Info=True;User ID=mihealteam-dev;Password=M9&#B-u!3%B4+9kW;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;").Options;
            var context = new InventoryContext(options);
            return context;
        }

        public static void Destroy(InventoryContext context)
        {
            context.Dispose();
        }
    }
}
