using System.Threading;
using NUnit.Framework;


namespace WebAddessbookTests
{
    [TestFixture]
    public class GroupRemovalTests : TestsBase
    {

        [Test]
        public void GroupCreationTest()
        {
            AppManager.Navigator.OpenStartPage();
            AccountData user = new AccountData("admin", "secret");
            AppManager.Auth.Login(user);
            AppManager.Group.GoToGroupPage();
            AppManager.Group.SelectGroup(1);
            AppManager.Group.DeleteGroup();
            AppManager.Group.GoToGroupPage();
            Thread.Sleep(5000);
            AppManager.Auth.logoff();
        }
    }
}