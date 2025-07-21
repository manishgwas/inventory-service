using Microsoft.VisualStudio.TestTools.UnitTesting;
using Inventory_Management_Service.Controllers;
using Inventory_Management_Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace InventoryManagementService.Tests
{
    [TestClass]
    public class ProductControllerTests
    {
        private InventoryDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<InventoryDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb_Product")
                .Options;
            var context = new InventoryDbContext(options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            return context;
        }

        [TestMethod]
        public async Task CreateProduct_ReturnsCreatedProduct()
        {
            var context = GetDbContext();
            var category = new Category { Name = "Cat1" };
            context.Categories.Add(category);
            context.SaveChanges();
            var controller = new ProductController(context);
            var product = new Product { Name = "TestProduct", CategoryId = category.Id, RowVersion = new byte[8] };

            var result = await controller.CreateProduct(product);
            var createdResult = result as CreatedAtActionResult;
            Assert.IsNotNull(createdResult);
            var createdProduct = createdResult.Value as Product;
            Assert.IsNotNull(createdProduct);
            Assert.AreEqual("TestProduct", createdProduct.Name);
        }

        [TestMethod]
        public async Task GetProducts_ReturnsAllProducts()
        {
            var context = GetDbContext();
            var category = new Category { Name = "Cat1" };
            context.Categories.Add(category);
            context.SaveChanges();
            context.Products.Add(new Product { Name = "Prod1", CategoryId = category.Id, RowVersion = new byte[8] });
            context.Products.Add(new Product { Name = "Prod2", CategoryId = category.Id, RowVersion = new byte[8] });
            context.SaveChanges();
            var controller = new ProductController(context);

            var result = await controller.GetProducts(null, null);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var value = okResult.Value;
            Assert.IsNotNull(value);
            var productsProp = value.GetType().GetProperty("products");
            Assert.IsNotNull(productsProp);
            var products = productsProp.GetValue(value) as IEnumerable<Product>;
            Assert.IsNotNull(products);
            Assert.IsTrue(products.Any());
        }
    }
}
