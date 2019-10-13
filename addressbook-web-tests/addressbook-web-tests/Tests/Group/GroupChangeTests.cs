using System.Threading;
using NUnit.Framework;

namespace WebAddessbookTests
{
    [TestFixture]
    public class GroupChangeTests : TestsBase
    {
        [Test]
        public void ChangeGroupTest()
        {
            GroupData newData = new GroupData("newGroupName");
            newData.GroupHeader = "newGroupHeder";
            newData.GroupFooter = "newGroupFooter";

            AppManager.Group.Modify(1, newData);
            AppManager.Auth.logoff();
        }
    }
}
