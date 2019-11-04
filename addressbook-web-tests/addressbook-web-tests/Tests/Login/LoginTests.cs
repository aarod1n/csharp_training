using System.Threading;
using NUnit.Framework;

namespace WebAddessbookTests
{
    [TestFixture]
    public class LoginTests : TestsBase
    {
        [Test]
        public void LoginByValidUser()
        {
            AppManager.Auth.Logoff();
            AccountData account = new AccountData("admin", "secret");
            AppManager.Auth.Login(account);
            Assert.IsTrue(AppManager.Auth.IsLoggetIn());
        }

        [Test]
        public void LoginByNotValid()
        {
            AppManager.Auth.Logoff();
            AccountData account = new AccountData("user", "secret");
            AppManager.Auth.Login(account);
            Assert.IsFalse(AppManager.Auth.IsLoggetIn());
        }

    }
}
