using System;
using System.IO;
using System.Threading;
using NUnit.Framework;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Excel = Microsoft.Office.Interop.Excel;

namespace WebAddessbookTests
{
    [TestFixture]
    public class GroupCerationTests : AuhtTestsBase
    {
        //Создаем список с объектами GroupData у которых случайным образом заполняются свойства.
        public static IEnumerable<GroupData> RandomGroupDataProvider()
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
            List<GroupData> groups = new List<GroupData>();
            string[] lines = File.ReadAllLines(@"groups.csv");
            foreach (string l in lines)
            {
                string[] parts = l.Split(',');
                groups.Add(new GroupData(parts[0])
                {
                    GroupHeader = parts[1],
                    GroupFooter = parts[2]
                });
            }

            return groups;
        }

        //Читаем из xml файла
        public static IEnumerable<GroupData> GroupDataFromXmlFile()
        {
           return (List<GroupData>)
                new XmlSerializer(typeof(List<GroupData>)).Deserialize(new StreamReader(@"groups.xml"));
        }

        //Читаем из json файла
        public static IEnumerable<GroupData> GroupDataFromJsonFile()
        {
            return JsonConvert.DeserializeObject<List<GroupData>>(File.ReadAllText(@"groups.json"));
        }


        //Читаем из excel файла
        public static IEnumerable<GroupData> GroupDataFromExcelFile()
        {
            List<GroupData> groups = new List<GroupData>();
            //Создаем приложение для работы с excel'em
            Excel.Application app = new Excel.Application();
            
            //Создаем новый док
            Excel.Workbook wb = app.Workbooks.Open(Path.Combine(Directory.GetCurrentDirectory(), @"groups.xlsx"));
            
            //Выбираем активную страницу
            Excel.Worksheet sheet = wb.ActiveSheet;
            
            //Берем область с данными
            Excel.Range range = sheet.UsedRange;
            for(int i =1; i <= range.Count; i++)
            {
                groups.Add(new GroupData()
                {
                    GroupName = range.Cells[i, 1].Value,
                    GroupHeader = range.Cells[i, 2].Value,
                    GroupFooter = range.Cells[i, 3].Value
                });
            }
            wb.Close();
            //app.Visible = false;
            app.Quit();
            return groups;
            
        }

        [Test, TestCaseSource("GroupDataFromJsonFile")]
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

        [Test]
        public void TestDBConnectivity()
        {
            DateTime start = DateTime.Now;
            List<GroupData> groupFromUI = AppManager.Group.GetGroupList();
            DateTime end = DateTime.Now;
            System.Console.Out.WriteLine(end.Subtract(start));

            start = DateTime.Now;
            List<GroupData> groupFromBD = GroupData.GetAll();
            end = DateTime.Now;
            System.Console.Out.WriteLine(end.Subtract(start));
        }
    }
}