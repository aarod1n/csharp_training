using System.Threading;
using NUnit.Framework;
using System.Collections.Generic;

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
            List<GroupData> oldGroupList = AppManager.Group.GetGroupList();
            
            //Запоминаем группу, которую будем изменять
            GroupData oldGroup = oldGroupList[0];
            
            AppManager.Group.Modify(0, newGroup);
            
            //Быстрая проверка
            Assert.AreEqual(oldGroupList.Count, AppManager.Group.GetGroupCount());

            List<GroupData> newGroupList = AppManager.Group.GetGroupList();
            AppManager.Group.CheckChangeGroupResultByObj(0, newGroup, oldGroupList, newGroupList);

            //Проверяем изменение имени по нашему Id
            foreach(GroupData g in newGroupList)
            {
                if(g.Id == oldGroup.Id)
                {
                    Assert.AreEqual(newGroup.GroupName, g.GroupName);
                }
            }

        }
    }
}
