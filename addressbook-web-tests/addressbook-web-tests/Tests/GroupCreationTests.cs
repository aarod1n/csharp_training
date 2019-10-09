using System.Threading;
using NUnit.Framework;


namespace WebAddessbookTests
{
    [TestFixture]
    public class GroupCerationTests : TestsBase
    {

        [Test]
        public void GroupCreationTest()
        {
            AppManager.Navigator.OpenStartPage();
            AccountData user = new AccountData("admin", "secret");
            AppManager.Auth.Login(user);
            AppManager.Group.GoToGroupPage();
            AppManager.Group.InitNewGroupCreation();
            GroupData group = new GroupData("test2name");
            group.GroupHeader = "test2header";
            group.GroupFooter = "test2footer";
            AppManager.Group.FillGroupForm(group);
            AppManager.Group.CreatedGroup();
            AppManager.Group.SubmitGroupCreation();
            AppManager.Group.GoToGroupPage();
            Thread.Sleep(5000);
            AppManager.Auth.logoff();
            }
    }
}