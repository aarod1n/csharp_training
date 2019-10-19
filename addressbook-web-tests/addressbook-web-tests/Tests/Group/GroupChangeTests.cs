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
            GroupData newData = new GroupData("newGroupName54545");
            newData.GroupHeader = "header123123123";
            newData.GroupFooter = null;

            AppManager.Group.Modify(3, newData);
        }
    }
}
