using electrigreenAPI.Authentication;
using electrigreenAPI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace UnitTestingLogin
{
    [TestClass]
    public class LoginTests
    {
        private Login _login;
        private AuthManager _authManagerMock;

        [TestInitialize]
        public void Setup()
        {
            var users = new List<RegisterModel>
            {
                new RegisterModel { email = "test@example.com", password = "password123" },
                new RegisterModel { email = "another@example.com", password = "test456" }

            };

            _authManagerMock = new AuthManager(users);

            _login = new Login(_authManagerMock);
        }


        [TestMethod]
        public async Task AuthenticateWithAPI_InvalidCredentials_ReturnsFalse()
        {
            string email = "invalid@example.com";
            string password = "wrongpassword";

            bool isAuthenticated = await _login.AuthenticateWithAPI(email, password);

            Assert.IsFalse(isAuthenticated);
        }
    }
}
