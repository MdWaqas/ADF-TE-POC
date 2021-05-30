using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADF_TE_POC.Models;
using Moq;

namespace ADF_TE_POC.ComponentTests
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
