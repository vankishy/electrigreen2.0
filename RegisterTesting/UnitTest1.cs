using System;
using System.Collections.Generic;
using System.IO;
using electrigreenAPI.Controllers;
using electrigreenAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace electrigreenAPI.Tests
{
    [TestClass]
    public class RegisterControllerTests
    {
        [TestMethod]
        //Memastikan pengguna terdaftar kedalam json
        public void GetUser_ReturnsOkResult_WhenUsersExist()
        {
            var controller = new RegisterController();
            controller.Register(new RegisterModel { nama = "John Doe", email = "john@example.com", password = "password123" });

            var result = controller.GetUser() as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsInstanceOfType(result.Value, typeof(List<RegisterModel>));
        }

        [TestMethod]
        //Memberi pesan ArgumentNullException ketika Register Null
        public void Register_ThrowsException_WhenModelIsNull()
        {
            var controller = new RegisterController();

            Assert.ThrowsException<ArgumentNullException>(() => controller.Register(null));
        }
    }
}
