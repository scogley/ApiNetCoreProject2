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

        public void AddTestUsers(int userCount)
        {
            for (int i = 0; i < userCount; i++)
            {
                UserModel user = new UserModel();
                user.Email = $"testuser{userCount}@gmail.com";
                user.Password = "foobar";
                var newUser = _service.Add(user);
            }
        }

        public UserModel AddTestUser()
        {   
            UserModel user = new UserModel();
            user.Email = $"testuser@gmail.com";
            user.Password = "foobar";
            var newUser = _service.Add(user);
            return newUser;
        }

        [TestMethod]
        public void GetSpecific_WhenCalledWithInvalidGuid_ReturnsNotFound()
        {
            // Arrange: do nothing. Do not add any users to "db".
            
            // Act: call Get {guid} 
            var result = (NotFoundObjectResult)_controller.Get(new System.Guid());

            // Assert: 404 Not Found since there are no users in "db".
            Assert.AreEqual(result.StatusCode, new NotFoundObjectResult("guid").StatusCode);
        }

        [TestMethod]
        public void GetSpecific_WhenCalledWithValidGuid_ReturnsOk()
        {
            // Arrange: add new user directly using _service.Add() and retrieve the resulting UserId.
            var newUser = AddTestUser();

            // Act: call Get {guid} and pass the valid UserId.
            var result = (OkObjectResult)_controller.Get(newUser.UserId);

            // Assert: Ok Object Result Code
            //Assert.AreEqual(result.StatusCode, new OkObjectResult("guid").StatusCode);

            result.StatusCode.Should().Be(new OkObjectResult("guid").StatusCode);
        }

        [TestMethod]
        public void Get_WhenCalled_ReturnsListObject()
        {
            // Arrange: Add new users directly using _service.Add()
            AddTestUsers(4);

            // Act: call Get
            var result = _controller.Get();

            result.Should().BeOfType<List<UserModel>>();
        }
    }
}
