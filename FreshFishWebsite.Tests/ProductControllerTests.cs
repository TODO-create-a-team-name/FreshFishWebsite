using FreshFishWebsite.Controllers;
using FreshFishWebsite.Interfaces;
using FreshFishWebsite.Repositories;
using FreshFishWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Microsoft.AspNetCore.Hosting;
using FreshFishWebsite.ViewModels;

namespace FreshFishWebsite.Tests
{
    public class ProductControllerTests
    {
        [Fact]
        public void IndexReturnsAViewResultWithAListOfTests()
        {

            // Arrange
            var product = new Mock<IRepository<Product>>();
            var storage = new Mock<IStorageRepository>();
            product.Setup(repo => repo.GetAll()).Returns(GetTestProducts());
            storage.Setup(storage => storage.GetAll()).Returns(GetTestStorages);
            var controller = new ProductsController(product.Object,storage.Object);

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Product>>(viewResult.Model);
            Assert.Equal(GetTestProducts().Count, model.Count());
        }
       private List<Product> GetTestProducts()
        {
            var products = new List<Product>
            {
                new Product { Id=1, ProductName="fish", QuantityKg = 100, PricePerKg = 100},
                new Product { Id=2, ProductName="fish2", QuantityKg = 200, PricePerKg = 200},
                new Product { Id=3, ProductName="fish3", QuantityKg = 300, PricePerKg = 300},
            };
            return products;
        } private List<Storage> GetTestStorages()
        {
            var storages = new List<Storage>
            {
                new Storage { Id=1, Address="strorage", StorageNumber = 1},
                new Storage { Id=2, Address="strorage2", StorageNumber = 2},
                new Storage { Id=3, Address="strorage3", StorageNumber = 3}
            };
            return storages;
        }

        [Fact]
        public void GetProductReturnsNotFoundResultWhenProductNotFound()
        {
            // Arrange
            int testSeedlingId = 1;
            var product = new Mock<IRepository<Product>>();
            product.Setup(repo => repo.GetById(testSeedlingId))
                .Returns(null as Product);
            var storage = new Mock<IStorageRepository>();
            var controller = new ProductsController(product.Object, storage.Object);

            // Act
            var result = controller.Edit(testSeedlingId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetProductReturnsViewResultWithProduct()
        {
            // Arrange
            int testSeedlingId = 1;
            var product = new Mock<IRepository<Product>>();
            product.Setup(repo => repo.GetById(testSeedlingId))
                .Returns(GetTestProducts().FirstOrDefault(p => p.Id == testSeedlingId));
            var storage = new Mock<IStorageRepository>();
            var controller = new ProductsController(product.Object, storage.Object);

            // Act
            var result = controller.Edit(testSeedlingId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Product>(viewResult.ViewData.Model);
            Assert.Equal("fish", model.ProductName);
            Assert.Equal(100, model.QuantityKg);
            Assert.Equal(testSeedlingId, model.Id);
        }
            /*
                    [Fact]
                    public void AddUserReturnsViewResultWithUserModel()
                    {
                        // Arrange
                        var product = new Mock<IRepository<Product>>();
                        var storage = new Mock<IStorageRepository>();
                        var controller = new ProductsController(product.Object,storage.Object);
                        controller.ModelState.AddModelError("ProductName", "Required");
                        Product newProduct = new Product();
                        ProductViewModel productViewModel = new ProductViewModel();

                        // Act
                        var result = controller.Create(1);
                        // Assert
                        var viewResult = Assert.IsType<ViewResult>(result);
                        Assert.Equal(productViewModel, viewResult?.Model);
                    }
                     [Fact]
                  public void AddSeedlingReturnsViewResultWithSeedlingModel()
                  {
                      // Arrange
                      var mock = new Mock<IRepository<Product>>();
                      var storage = new Mock<IStorageRepository>();
                      var controller = new ProductsController(mock.Object, storage.Object);
                      controller.ModelState.AddModelError("Name", "Required");
                      Product newProduct = new Product();
                      ProductViewModel productViewModel = new ProductViewModel();
                      productViewModel.StorageId = 1;
                      // Act
                      var result = controller.Create(productViewModel);

                      // Assert
                      var viewResult = Assert.IsType<ViewResult>(result);
                      Assert.Equal(productViewModel, viewResult?.Model);
                  }
            */
        }
}
