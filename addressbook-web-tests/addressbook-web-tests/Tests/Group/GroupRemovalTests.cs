using System.Threading;
using NUnit.Framework;
using System.Collections.Generic;


namespace WebAddessbookTests
{
    [TestFixture]
    public class GroupRemovalTests : GroupTestBase
    {

        [Test]
        public void GroupRemovalTest()
        {
            GroupData group = new GroupData("test123Removoalname");
            group.GroupHeader = "test123Removoalheader";
            group.GroupFooter = "test123Removoalfooter";

            AppManager.Group.CheckPresenceGroup(group);
            List<GroupData> oldGroupList = GroupData.GetAll();
                        
            //Сохраняем ту группу что будем удалять
            GroupData removalGroup = oldGroupList[0];

            AppManager.Group.RemovaGroup(removalGroup);
            
            //Быстрая проверка
            Assert.AreEqual(oldGroupList.Count - 1, AppManager.Group.GetGroupCount());

            List<GroupData> newGroupList = GroupData.GetAll();

            oldGroupList.RemoveAt(0);
            AppManager.Group.CheckGroupResultByObj(oldGroupList, newGroupList);

            //Проверяем, что в новом списке групп, нету группы с идентификитором удаленной.
            foreach(GroupData g in newGroupList)
            {
                Assert.AreNotEqual(g.Id, removalGroup.Id);
            }
        }
    }
}