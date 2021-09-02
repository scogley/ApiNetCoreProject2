using Microsoft.VisualStudio.TestTools.UnitTesting;
using ApiNetCoreProject2;
using ApiNetCoreProject2.Models;
using ApiNetCoreProject2.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using FluentAssertions;

namespace ApiNetCoreProject2.Tests
{
    [TestClass]
    public class UserControllerTest
    {
        UserController _controller;
        IUserManagerService _service;
        public UserControllerTest()
        {
            _service = new UserManagerService();
            _controller = new UserController(_service);
        }


        [TestMethod]
        public void GetSpecific_WhenCalledWithInvalidGuid_ReturnsNotFound()
        {
            // Arrange: do not add any users to db
            
            // Act: call Get {guid} 
            var result = (NotFoundObjectResult)_controller.Get(new System.Guid());

            // Assert: 404 Not Found since there are no users in db
            Assert.AreEqual(result.StatusCode, new NotFoundObjectResult("guid").StatusCode);
        }

        [TestMethod]
        public void GetSpecific_WhenCalledWithValidGuid_ReturnsOk()
        {
            // Arrange: add new user directly using _service.Add() and retrieve the resulting UserId.
            UserModel user = new UserModel();
            user.Email = "smcogley@gmail.com";
            user.Password = "foobar";
            var newUser = _service.Add(user);
            
            
            // Act: call Get {guid} and pass the valid UserId.
            var result = (OkObjectResult)_controller.Get(newUser.UserId);

            // Assert: Ok Object Result Code
            //Assert.AreEqual(result.StatusCode, new OkObjectResult("guid").StatusCode);

            result.StatusCode.Should().Be(new OkObjectResult("guid").StatusCode);
        }

        [TestMethod]
        public void TestMethod1()
        {
            // Arrange and Act
            UserModel user1 = new UserModel();
            user1.Email = "smcogley@gmail.com";

            UserModel user2 = new UserModel();
            user2.Email = "smcogley@gmail.com";
            
            // Assert
            Assert.AreEqual(user1.Email, user2.Email);
        }
    }
}
