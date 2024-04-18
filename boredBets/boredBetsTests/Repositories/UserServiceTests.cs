using Microsoft.VisualStudio.TestTools.UnitTesting;
using boredBets.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using boredBets.Models.Dtos;
using boredBets.Models;
using boredBets.Repositories.Interface;
using Moq;
using boredBets.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace boredBets.Repositories.Tests
{
    [TestClass()]
    public class UserServiceTests
    {
        [TestMethod()]
        public async Task GetByUserIdTest()
        {
            // Arrange
            var userId = Guid.Parse("164a1ba9-5b1f-4295-85fc-87d802c0a2f2");
            var expectedUser = new
            {
                Id = userId,
                Email = "admin@admin.admin",
                Wallet = 42069,
                Admin = true,
                Created = DateTime.Parse("2024 - 03 - 25 09:53:43.921082")
            };

            var mockUserInterface = new Mock<IUserInterface>();
            mockUserInterface.Setup(x => x.GetByUserId(userId)).ReturnsAsync(expectedUser);

            var controller = new UserController(mockUserInterface.Object);

            // Act
            var result = await controller.GetByUserId(userId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            var okResult = (OkObjectResult)result.Result;
            Assert.AreEqual(expectedUser, okResult.Value);
        }

        


    }
}