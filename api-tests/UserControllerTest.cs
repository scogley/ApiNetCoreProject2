using System;
using Xunit;
using ApiNetCoreProject2;
using ApiNetCoreProject2.Controllers;
using Microsoft.AspNetCore.Mvc;
using ApiNetCoreProject2.Models;

namespace Xunit
{
    public class UserControllerTest
    {
        UserController _controller;
        IUserManagerService _service;

        public UserControllerTest()
        {
            _service = new FakeUserManagerService();
            _controller = new UserController(_service);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsOkResult()
        {
            // Arrange
            var value = new UserModel();
            value.Email = "smcogley@gmail.com";
            value.Password = "foobar";
            _service.Add(value);
            
            // Act
            var okResult = _controller.Get();

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }
    }
}
