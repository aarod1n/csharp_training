using System.Threading;
using NUnit.Framework;
using System.Collections.Generic;

namespace WebAddessbookTests
{
    [TestFixture]
    public class GroupCerationTests : AuhtTestsBase
    {

        [Test]
        public void GroupCreationTest()
        {
            GroupData group = new GroupData("test5454542name");
            group.GroupHeader = "test2header";
            group.GroupFooter = "test2footer";
            List<GroupData> oldGroupList = AppManager.Group.GetGroupList();
            AppManager.Group.Created(group);
            List<GroupData> newGroupList = AppManager.Group.GetGroupList();
            oldGroupList.Add(group);
            AppManager.Group.CheckGroupResultByObj(oldGroupList, newGroupList);
        }
    }
}