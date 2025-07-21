using Microsoft.VisualStudio.TestTools.UnitTesting;
using Inventory_Management_Service.Controllers;
using Inventory_Management_Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace InventoryManagementService.Tests
{
    [TestClass]
    public class CategoryControllerTests
    {
        private InventoryDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<InventoryDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb_Category")
                .Options;
            var context = new InventoryDbContext(options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            return context;
        }

        [TestMethod]
        public async Task CreateCategory_ReturnsCreatedCategory()
        {
            var context = GetDbContext();
            var controller = new CategoryController(context);
            var category = new Category { Name = "TestCategory" };

            var result = await controller.CreateCategory(category);
            var createdResult = result as CreatedAtActionResult;
            Assert.IsNotNull(createdResult);
            var createdCategory = createdResult.Value as Category;
            Assert.IsNotNull(createdCategory);
            Assert.AreEqual("TestCategory", createdCategory.Name);
        }

        [TestMethod]
        public async Task GetCategories_ReturnsAllCategories()
        {
            var context = GetDbContext();
            context.Categories.Add(new Category { Name = "Cat1" });
            context.Categories.Add(new Category { Name = "Cat2" });
            context.SaveChanges();
            var controller = new CategoryController(context);

            var result = await controller.GetCategories();
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var categories = okResult.Value as IEnumerable<Category>;
            Assert.IsNotNull(categories);
            Assert.IsTrue(categories.Count() >= 2); // Accepts seeded data
        }
    }
}
