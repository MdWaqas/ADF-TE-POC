using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ADF_TE_POC.Controllers;
using ADF_TE_POC.DTOs;
using ADF_TE_POC.Models;
using ADF_TE_POC.Services.Impl;
using Microsoft.AspNetCore.Mvc;
using Shouldly;
using Xunit;

namespace ADF_TE_POC.IntegrationTests.Controllers
{
    public class ProductsControllerTests : ControllerTestBase
    {
        private readonly ProductsController _productsController;

        public ProductsControllerTests()
        {
            _productsController = new ProductsController(new ProductService(Context));
        }

        [Fact]
        public async Task GET_ShouldReturn_AllProducts()
        {
            var resp = await _productsController.Get();

            resp.Result.ShouldBeAssignableTo<ObjectResult>();

            var products = ((IEnumerable<Product>)((OkObjectResult)resp.Result).Value);
            var dbProducts = Context.Products.ToList();

            products.Count().ShouldBe(dbProducts.Count);
        }


        [Fact]
        public async Task GET_ShouldReturn_ProductById()
        {
            var resp = await _productsController.Get(2);

            resp.Result.ShouldBeAssignableTo<OkObjectResult>();

            var product = ((Product)((OkObjectResult)resp.Result).Value);
            product.ShouldNotBeNull();
        }

        [Fact]
        public async Task GET_ShouldReturn_BadRequest()
        {
            var resp = await _productsController.Get(101);

            resp.Result.ShouldBeAssignableTo<NotFoundObjectResult>();
        }

        [Fact]
        public async Task POST_ShouldAdd_Product()
        {
            var resp = await _productsController.Post(new ProductRequest
            {
                Name = "Lux",
                Color = "Red",
                AvailableQuantity = 10,
                Category = "Soap",
                UnitPrice = 10
            });
            resp.Result.ShouldBeAssignableTo<OkObjectResult>();
            var product = ((Product)((OkObjectResult)resp.Result).Value);
            product.ShouldNotBeNull();

            var dbProduct = Context.Products.FirstOrDefault(t => t.Id == product.Id);
            dbProduct.ShouldNotBeNull();
        }

        [Fact]
        public async Task PUT_ShouldUpdate_Product()
        {
            var resp = await _productsController.Put(2, new ProductRequest
            {
                Name = "Lux",
                Color = "Red",
                AvailableQuantity = 10,
                Category = "Soap",
                UnitPrice = 10
            });
            resp.ShouldBeAssignableTo<OkResult>();

            var dbProduct = Context.Products.FirstOrDefault(t => t.Id == 2);
            dbProduct.ShouldNotBeNull();
            dbProduct.Color.ShouldBeEquivalentTo("Red");
        }

        [Fact]
        public async Task PUT_ShouldReturn_NotFound()
        {
            var resp = await _productsController.Put(101, new ProductRequest
            {
                Name = "Lux",
                Color = "Red",
                AvailableQuantity = 10,
                Category = "Soap",
                UnitPrice = 10
            });
            resp.ShouldBeAssignableTo<NotFoundObjectResult>();
        }

        [Fact]
        public async Task DELETE_ShouldReturn_NotFound()
        {
            var resp = await _productsController.Delete(101);
            resp.ShouldBeAssignableTo<NotFoundObjectResult>();
        }

        //[Fact]
        //public async Task DELETE_ShouldDelete_NotProduct()
        //{
        //    var resp = await _productsController.Delete(1);
        //    resp.ShouldBeAssignableTo<NoContentResult>();
        //}
    }
}