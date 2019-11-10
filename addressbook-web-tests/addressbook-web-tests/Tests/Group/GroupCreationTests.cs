using System.Threading;
using NUnit.Framework;
using System.Collections.Generic;

namespace WebAddessbookTests
{
    [TestFixture]
    public class GroupCerationTests : AuhtTestsBase
    {
        //Создаем список с объектами GroupData у которых случайным образом заполняются свойства.
        public static IEnumerable<GroupData> RondomGroupDataProvider()
        {
            List<GroupData> group = new List<GroupData>();
            for(int i = 0; i < 5; i++)
            {
                group.Add(new GroupData()
                {
                    GroupHeader = GenerationRandomString(5, false),
                    GroupName = GenerationRandomString(10, false),
                    GroupFooter = GenerationRandomString(10, false)
                });
            }
            return group;
        }

        [Test, TestCaseSource("RondomGroupDataProvider")]
        public void GroupCreationTest(GroupData group)
        {
            List<GroupData> oldGroupList = AppManager.Group.GetGroupList();
            AppManager.Group.Created(group);

            //Быстрая проверка
            Assert.AreEqual(oldGroupList.Count + 1, AppManager.Group.GetGroupCount());

            List<GroupData> newGroupList = AppManager.Group.GetGroupList();
            oldGroupList.Add(group);
            AppManager.Group.CheckGroupResultByObj(oldGroupList, newGroupList);
        }
    }
}