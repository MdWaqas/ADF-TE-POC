using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ADF_TE_POC.Models;
using Microsoft.EntityFrameworkCore;

namespace ADF_TE_POC.Services.Impl
{
    public class ProductService : IProductService
    {
        private readonly InventoryContext _inventory;

        public ProductService(InventoryContext inventory)
        {
            _inventory = inventory;
        }

        public async Task<List<Product>> Get()
        {
            return await _inventory.Products.ToListAsync();
        }

        public async Task<Product> Get(int id)
        {
            return await _inventory.Products.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Product> Insert(Product product)
        {
            await _inventory.Products.AddAsync(product);
            return product;
        }

        public async Task<bool> Update(Product product)
        {
            var prod = await _inventory.Products.FirstOrDefaultAsync(t => t.Id == product.Id);
            if (prod != null)
            {
                prod.Name = product.Name;
                prod.Category = product.Category;
                prod.AvailableQuantity = product.AvailableQuantity;
                prod.Color = product.Color;
                prod.UnitPrice = product.UnitPrice;
                await _inventory.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> Delete(int id)
        {
            var prod = await _inventory.Products.FirstOrDefaultAsync(t => t.Id == id);
            if (prod != null)
            {
                _inventory.Products.Remove(prod);
                await _inventory.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
