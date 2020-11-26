using AuthService.Models;
using AuthService.Repository;
using NUnit.Framework;

namespace AuthTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var userRepo = new LoginRepository();
            var user = userRepo.GetUser("moana");
            var expected = new User { Username = "moana", Password = "pmoana" };
            Assert.AreEqual(user.Password, expected.Password);
        }
    }
}