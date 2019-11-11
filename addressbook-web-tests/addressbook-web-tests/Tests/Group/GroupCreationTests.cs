using System.IO;
using System.Threading;
using NUnit.Framework;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

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
                group.Add(new GroupData(GenerationRandomString(10))
                {
                    GroupHeader = GenerationRandomString(5),                    
                    GroupFooter = GenerationRandomString(10)
                });
            }
            return group;
        }

        //Читаем данные из файла csv
        public static IEnumerable<GroupData> GroupDataFromCsvFile()
        {
            List<GroupData> group = new List<GroupData>();
            string[] lines = File.ReadAllLines(@"groups.csv");
            foreach (string l in lines)
            {
                string[] parts = l.Split(',');
                group.Add(new GroupData(parts[0])
                {
                    GroupHeader = parts[1],
                    GroupFooter = parts[2]
                });
            }

            return group;
        }

        //Читаем из xml файла
        public static IEnumerable<GroupData> GroupDataFromXmlFile()
        {
           return (List<GroupData>)
                new XmlSerializer(typeof(List<GroupData>)).Deserialize(new StreamReader(@"groups.xml"));
        }

        [Test, TestCaseSource("GroupDataFromXmlFile")]
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