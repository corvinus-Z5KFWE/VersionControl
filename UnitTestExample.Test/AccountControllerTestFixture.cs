using NUnit.Framework;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnitTestExample.Controllers;

namespace UnitTestExample.Test
{
    public class AccountControllerTestFixture
    {
        [
         Test,
         TestCase("abcd1234", false),
         TestCase("irf@uni-corvinus", false),
         TestCase("irf.uni-corvinus.hu", false),
         TestCase("irf@uni-corvinus.hu", true)
        ]
        public void TestValidateEmail(string email, bool expectedResults)
        {
            // Arrange
            var accountController = new AccountController();

            //Act
            var actualResult = accountController.ValidateEmail(email);

            //Assert
            Assert.AreEqual(expectedResults, actualResult);
        }

        [Test,
         TestCase("Password", false),
         TestCase("PASSWORD1", false),
         TestCase("password1", false),
         TestCase("pwd", false),
         TestCase("Password1", true)
         ]
        public bool TestValidatePassword(string password)
        {
            //Arrange
            var accountController = new AccountController();

            //Act

            
           
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasNumber = new Regex(@"[0-9]+");
            var isEightChar = new Regex(@".{8}");

            return hasLowerChar.IsMatch(password) && hasUpperChar.IsMatch(password) && hasNumber.IsMatch(password) && isEightChar.IsMatch(password);
            
        }

        [
            Test,
            TestCase("irf@uni-corvinus.hu", "Abcd1234"),
            TestCase("irf@uni-corvinus.hu", "Abcd1234567"),
            TestCase("irf@uni-corvinus.hu", "Password11")
        ]
        public void TestRegisterHappyPath(string email, string password)
        {
            // Arrange
            var accountController = new AccountController();

            // Act
            var actualResult = accountController.Register(email, password);

            // Assert
            Assert.AreEqual(email, actualResult.Email);
            Assert.AreEqual(password, actualResult.Password);
            Assert.AreNotEqual(Guid.Empty, actualResult.ID);
        }



        [
                Test,
                TestCase("irf@uni-corvinus", "Abcd1234"),
                TestCase("irf.uni-corvinus.hu", "Abcd1234"),
                TestCase("irf@uni-corvinus.hu", "abcd1234"),
                TestCase("irf@uni-corvinus.hu", "ABCD1234"),
                TestCase("irf@uni-corvinus.hu", "abcdABCD"),
                TestCase("irf@uni-corvinus.hu", "Ab1234"),
        ]
        public void TestRegisterValidateException(string email, string password)
        {
            // Arrange
            var accountController = new AccountController();

            // Act
            try
            {
                var actualResult = accountController.Register(email, password);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<ValidationException>(ex);
            }

            // Assert
        }

    }
}
