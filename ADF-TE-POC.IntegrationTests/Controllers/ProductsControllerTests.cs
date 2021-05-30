using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ADF_TE_POC.Controllers;
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
        public async Task ShouldReturn_All_Products()
        {
            var resp = await _productsController.Get();

            resp.Result.ShouldBeAssignableTo<ObjectResult>();

            var products = ((IEnumerable<Product>) ((OkObjectResult) resp.Result).Value);
            var dbProducts = Context.Products.ToList();

            products.Count().ShouldBe(dbProducts.Count);
        }
    }
}