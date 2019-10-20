using System.Threading;
using NUnit.Framework;

namespace WebAddessbookTests
{
    [TestFixture]
    public class GroupChangeTests : AuhtTestsBase
    {
        [Test]
        public void ChangeGroupTest()
        {
            GroupData group = new GroupData("test123name");
            group.GroupHeader = "test123header";
            group.GroupFooter = "test123footer";            

            GroupData newGroup = new GroupData("NewGroupname");
            newGroup.GroupHeader = "NewGroupheader";
            newGroup.GroupFooter = "NewGroupfooter";

            AppManager.Group.CheckPresenceGroup(group);
            AppManager.Group.Modify(1, newGroup);
        }
    }
}
