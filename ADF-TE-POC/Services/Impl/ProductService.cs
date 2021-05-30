using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ADF_TE_POC.Models;
using Microsoft.EntityFrameworkCore;

namespace ADF_TE_POC.Services.Impl
{
    /// <summary>
    /// Product Service
    /// </summary>
    /// <seealso cref="ADF_TE_POC.Services.IProductService" />
    public class ProductService : IProductService
    {
        /// <summary>
        /// The inventory
        /// </summary>
        private readonly InventoryContext _inventory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductService"/> class.
        /// </summary>
        /// <param name="inventory">The inventory.</param>
        public ProductService(InventoryContext inventory)
        {
            _inventory = inventory;
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Product>> Get()
        {
            return await _inventory.Products.ToListAsync();
        }

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<Product> Get(int id)
        {
            return await _inventory.Products.FirstOrDefaultAsync(t => t.Id == id);
        }

        /// <summary>
        /// Inserts the specified product.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <returns></returns>
        public async Task<Product> Insert(Product product)
        {
            await _inventory.Products.AddAsync(product);
            await _inventory.SaveChangesAsync();
            return product;
        }

        /// <summary>
        /// Updates the specified product.
        /// </summary>
        /// <param name="product">The product.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
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
