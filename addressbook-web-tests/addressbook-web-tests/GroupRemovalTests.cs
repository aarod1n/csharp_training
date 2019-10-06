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
            OpenStartPage();
            AccountData user = new AccountData("admin", "secret");
            Login(user);
            GoToGroupPage();
            SelectGroup(1);
            DeleteGroup();
            GoToGroupPage();
            Thread.Sleep(5000);
        }
    }
}