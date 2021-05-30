using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ADF_TE_POC.Models;

namespace ADF_TE_POC.Services
{
    public interface IProductService
    {
        Task<List<Product>> Get();
        Task<Product> Get(int id);
        Task<Product> Insert(Product product);
        Task<bool> Update(Product product);
        Task<bool> Delete(int id);
    }
}
