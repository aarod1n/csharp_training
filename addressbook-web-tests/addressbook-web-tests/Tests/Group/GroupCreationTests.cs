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
            GroupData group = new GroupData("test2name");
            group.GroupHeader = "test2header";
            group.GroupFooter = "test2footer";

            AppManager.Group.Created(group);
            AppManager.Auth.logoff();
            }
    }
}