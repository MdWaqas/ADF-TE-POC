using System;
using ADF_TE_POC.Models;

namespace ADF_TE_POC.IntegrationTests
{
    public class ControllerTestBase : IDisposable
    {
        public InventoryContext Context { get; }
        public ControllerTestBase()
        {
            Context = InventoryContextFactory.Create();

        }

        public void Dispose()
        {
            InventoryContextFactory.Destroy(Context);
        }
    }
}
