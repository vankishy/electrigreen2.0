using NUnit.Framework;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using electrigreen;

[TestFixture]
public class RegisterTests
{
    [Test]
    public void Reg_WithValidInput_ReturnsUser()
    {
        // Arrange
        var register = new Register();
        string expectedName = "John";
        string expectedEmail = "john@example.com";
        string expectedPassword = "password";

        // Act
        User user = register.RegAsync(expectedName, expectedEmail, expectedPassword).Result;

        // Assert
        Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(user);
        Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expectedName, user.Nama);
        Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expectedEmail, user.Email);
        Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expectedPassword, user.Password);
    }

    [Test]
    public void Reg_WithEmptyName_ThrowsArgumentException()
    {
        // Arrange
        var register = new Register();
        string invalidName = "";

        // Act & Assert
        Assert.ThrowsAsync<ArgumentException>(() => register.RegAsync(invalidName, "john@example.com", "password"));
    }

    [Test]
    public void Reg_WithEmptyEmail_ThrowsArgumentException()
    {
        // Arrange
        var register = new Register();
        string invalidEmail = "";

        // Act & Assert
        Assert.ThrowsAsync<ArgumentException>(() => register.RegAsync("John", invalidEmail, "password"));
    }

    [Test]
    public void Reg_WithEmptyPassword_ThrowsArgumentException()
    {
        // Arrange
        var register = new Register();
        string invalidPassword = "";

        // Act & Assert
        Assert.ThrowsAsync<ArgumentException>(() => register.RegAsync("John", "john@example.com", invalidPassword));
    }

    [Test]
    public void Reg_WithMismatchedPasswords_ThrowsInvalidOperationException()
    {
        // Arrange
        var register = new Register();

        // Act & Assert
        Assert.ThrowsAsync<InvalidOperationException>(() => register.RegAsync("John", "john@example.com", "password", "wrongpassword"));
    }
}
