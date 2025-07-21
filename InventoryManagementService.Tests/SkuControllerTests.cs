using Microsoft.VisualStudio.TestTools.UnitTesting;
using Inventory_Management_Service.Controllers;
using Inventory_Management_Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace InventoryManagementService.Tests
{
    [TestClass]
    public class SkuControllerTests
    {
        private InventoryDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<InventoryDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb_SKU")
                .Options;
            var context = new InventoryDbContext(options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            return context;
        }

        [TestMethod]
        public async Task CreateSku_ReturnsCreatedSku()
        {
            var context = GetDbContext();
            var category = new Category { Name = "Cat1" };
            context.Categories.Add(category);
            context.SaveChanges();
            var product = new Product { Name = "Prod1", CategoryId = category.Id, RowVersion = new byte[8] };
            context.Products.Add(product);
            context.SaveChanges();
            var controller = new SkuController(context);
            var sku = new SKU { Code = "SKU001", ProductId = product.Id, RowVersion = new byte[8] };

            var result = await controller.CreateSku(product.Id, sku);
            var createdResult = result as CreatedAtActionResult;
            Assert.IsNotNull(createdResult);
            var createdSku = createdResult.Value as SKU;
            Assert.IsNotNull(createdSku);
            Assert.AreEqual("SKU001", createdSku.Code);
        }

        [TestMethod]
        public async Task GetSkusForProduct_ReturnsAllSkus()
        {
            var context = GetDbContext();
            var category = new Category { Name = "Cat1" };
            context.Categories.Add(category);
            context.SaveChanges();
            var product = new Product { Name = "Prod1", CategoryId = category.Id, RowVersion = new byte[8] };
            context.Products.Add(product);
            context.SaveChanges();
            context.SKUs.Add(new SKU { Code = "SKU1", ProductId = product.Id, RowVersion = new byte[8] });
            context.SKUs.Add(new SKU { Code = "SKU2", ProductId = product.Id, RowVersion = new byte[8] });
            context.SaveChanges();
            var controller = new SkuController(context);

            var result = await controller.GetSkusForProduct(product.Id);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var skus = okResult.Value as IEnumerable<SKU>;
            Assert.IsNotNull(skus);
            Assert.IsTrue(skus.Count() >= 2); // Accepts seeded data
        }
    }
}
