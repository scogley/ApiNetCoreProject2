using Microsoft.VisualStudio.TestTools.UnitTesting;
using ApiNetCoreProject2;
using ApiNetCoreProject2.Models;

namespace ApiNetCoreProject2.Tests
{
    [TestClass]
    public class UnitTest1
    {
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
