using System.Threading;
using NUnit.Framework;
using System.Collections.Generic;


namespace WebAddessbookTests
{
    [TestFixture]
    public class GroupRemovalTests : AuhtTestsBase
    {

        [Test]
        public void GroupRemovalTest()
        {
            GroupData group = new GroupData("test123Removoalname");
            group.GroupHeader = "test123Removoalheader";
            group.GroupFooter = "test123Removoalfooter";

            AppManager.Group.CheckPresenceGroup(group);
            
            List<GroupData> oldGroupList = AppManager.Group.GetGroupList();
            AppManager.Group.RemovaGroup(0);
            List<GroupData> newGroupList = AppManager.Group.GetGroupList();
            oldGroupList.RemoveAt(0);
            AppManager.Group.CheckGroupResultByObj(oldGroupList, newGroupList);
        }
    }
}