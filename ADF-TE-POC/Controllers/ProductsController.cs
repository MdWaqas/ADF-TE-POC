using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Threading.Tasks;
using ADF_TE_POC.DTOs;
using ADF_TE_POC.Models;
using ADF_TE_POC.Services;

namespace ADF_TE_POC.Controllers
{
    /// <summary>
    /// Products Controller
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductsController"/> class.
        /// </summary>
        /// <param name="productService">The product service.</param>
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            var products = await _productService.Get();
            if (products != null && products.Any())
            {
                return Ok(value: products);
            }
            else
            {
                return NoContent();
            }
        }

        // GET api/<ValuesController>/5
        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            var product = await _productService.Get(id);
            if (product != null)
            {
                return Ok(product);
            }
            else
            {
                return NotFound($"Product with Id: {id} not found in the database.");
            }
        }

        /// <summary>
        /// Posts the specified product request.
        /// </summary>
        /// <param name="productRequest">The product request.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Product>> Post([FromBody] ProductRequest productRequest)
        {
            var product = new Product
            {
                Id = 0,
                Name = productRequest.Name,
                Color = productRequest.Color,
                AvailableQuantity = productRequest.AvailableQuantity,
                Category = productRequest.Category,
                UnitPrice = productRequest.UnitPrice
            };
            var prod = await _productService.Insert(product);
            if (prod != null)
            {
                return Ok(prod);
            }
            else
            {
                return BadRequest("Unable to insert the product into database.");
            }
        }

        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="productRequest">The product request.</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ProductRequest productRequest)
        {
            var isPresent = await _productService.Get(id);
            if (isPresent == null)
                return NotFound($"Product with Id: {id} not found in the database.");
            var product = new Product
            {
                Id = id,
                Name = productRequest.Name,
                Color = productRequest.Color,
                AvailableQuantity = productRequest.AvailableQuantity,
                Category = productRequest.Category,
                UnitPrice = productRequest.UnitPrice
            };
            await _productService.Update(product);
            return Ok();
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var isPresent = await _productService.Get(id);
            if (isPresent == null)
                return NotFound($"Product with Id: {id} not found in the database.");
            await _productService.Delete(id);
            return NoContent();
        }
    }
}
