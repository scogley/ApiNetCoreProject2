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
            user.Email = "testuser@gmail.com";
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

        [TestMethod]
        public void Post_WhenCalled_ReturnsCreated()
        {
            // Arrange: create a User that would be passed in message body
            UserModel value = new UserModel();
            value.Email = "testuser@gmail.com";
            value.Password = "foobar";

            // Act: call Post
            var result = (CreatedAtActionResult)_controller.Post(value);

            result.Should().BeOfType<CreatedAtActionResult>();
        }

        [TestMethod]
        public void Put_WhenCalledWithInvalidGuid_ReturnsNotFoundResult()
        {
            // Arrange: create a user value
            UserModel value = new UserModel();
            value.Email = "testuser@gmail.com";
            value.Password = "foobar";

            // Act: call Put with the user value to update
            var result = (NotFoundResult)_controller.Put(new System.Guid(), value);

            // Assert: 404 Not Found since the guid passed was not found in "db".
            //Assert.AreEqual(result.StatusCode, new NotFoundResult().StatusCode);
            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void Put_WhenCalledWithValidGuid_ReturnsOk()
        {
            // Arrange: add new user directly using _service.Add() and retrieve the resulting UserId.
            var newUser = AddTestUser();

            // Act: call Put and pass the valid UserId.
            var result = (OkObjectResult)_controller.Put(newUser.UserId, newUser);

            // Assert: Ok Object Result Code
            //Assert.AreEqual(result.StatusCode, new OkObjectResult("guid").StatusCode);

            result.StatusCode.Should().Be(new OkObjectResult("guid").StatusCode);
        }

        [TestMethod]
        public void Delete_WhenCalledWithValidGuid_ReturnsOk()
        {
            // Arrange: add new user directly using _service.Add() and retrieve the resulting UserId.
            var newUser = AddTestUser();

            // Act: call Put and pass the valid UserId.
            var result = (OkResult)_controller.Delete(newUser.UserId);

            // Assert: Ok Object Result Code
            result.StatusCode.Should().Be(new OkResult().StatusCode);
        }

        [TestMethod]
        public void Delete_WhenCalledWithInvalidGuid_ReturnsNotFoundObjectResult()
        {
            // Arrange: add new user directly using _service.Add() and retrieve the resulting UserId.
            var newUser = AddTestUser();

            // Act: call Delete and pass a newly generated guid (invalid) UserId.
            var result = (NotFoundObjectResult)_controller.Delete(new System.Guid());

            // Assert: NotFoundObjectResult Code
            result.StatusCode.Should().Be(new NotFoundObjectResult("foo").StatusCode);
        }
    }
}
