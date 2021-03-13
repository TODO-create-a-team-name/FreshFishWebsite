using FreshFishWebsite.Controllers;
using FreshFishWebsite.Interfaces;
using FreshFishWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using NUnit.Framework;
using Xunit;
using System.Data.Entity;
using System.Linq;
using System.Data.Entity.Infrastructure;

namespace FreshFishWebsite.Tests
{
    public class DriverControllerTests
    {
        [Fact]
        public void GetAllBlogs_orders_by_name()
        {
            var data = new List<Driver>
            {
                new Driver { Name = "BBB" },
                new Driver { Name = "ZZZ" },
                new Driver { Name = "AAA" },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Driver>>();
            mockSet.As<IDbAsyncEnumerable<Driver>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<Driver>(data.GetEnumerator()));

            mockSet.As<IQueryable<Driver>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<Driver>(data.Provider));

            mockSet.As<IQueryable<Driver>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Driver>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Driver>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<FreshFishDbContext>();
           // mockContext.Setup(c => c.Drivers).Returns(mockSet.Object);

            //var service = new BlogService(mockContext.Object);
            //var blogs = await service.GetAllBlogsAsync();

            //Assert.AreEqual(3, blogs.Count);
            //Assert.AreEqual("AAA", blogs[0].Name);
            //Assert.AreEqual("BBB", blogs[1].Name);
            //Assert.AreEqual("ZZZ", blogs[2].Name);
        }
        }
    }

