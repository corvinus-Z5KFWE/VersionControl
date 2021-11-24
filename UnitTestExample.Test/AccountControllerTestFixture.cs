using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestExample.Controllers;

namespace UnitTestExample.Test
{
    class AccountControllerTestFixture
    {
        [Test]
        public void TestvalidateEmail(string email, bool expectedResults)
        {
            // Arrange
            var accountController = new AccountController();

            //Act
            var acrualResult = accountController.ValidateEmail(email);

            //Assert
            Assert.AreEqual(expectedResults, acrualResult);
        }
    }
}
