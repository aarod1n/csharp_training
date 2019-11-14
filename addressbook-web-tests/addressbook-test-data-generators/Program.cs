using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Excel = Microsoft.Office.Interop.Excel;
using WebAddessbookTests;

namespace addressbook_test_data_generators
{
    class Program
    {
        static void Main(string[] args)
        {
            string typeData = args[0];
            int count = Convert.ToInt32(args[1]);
            string fileName = args[2];
            string format = args[3];
            
            //Список для формирования сгенеренных данных групп
            List<GroupData> groups = new List<GroupData>();
            //Список для формирования сгенеренных данных контактов
            List<EntryDate> contacts = new List<EntryDate>();

            //В цикле наполняем список данными
            if (typeData == "groups")
            {
                for (int i = 0; i < count; i++)
                {
                    groups.Add(new GroupData(TestsBase.GenerationRandomString(10))
                    {
                        GroupHeader = TestsBase.GenerationRandomString(10),
                        GroupFooter = TestsBase.GenerationRandomString(10)
                    });
                }
            }
            else if (typeData == "contacts")
            {
                for (int i = 0; i < count; i++)
                {
                    contacts.Add(new EntryDate()
                    {
                        FirstName = TestsBase.GenerationRandomString(10),
                        MiddleName = TestsBase.GenerationRandomString(10),
                        LastName = TestsBase.GenerationRandomString(10),
                        Address = TestsBase.GenerationRandomString(10)
                    });
                }
            }
            else
            {
                Console.Out.Write("Unknown type " + typeData);
            }            

            if(format == "excel")
            {
                WriteGroupsToExcelFile(groups, fileName);
            }
            else
            {
                //Открываем на запись
                StreamWriter writer = new StreamWriter(fileName);
                if (format == "csv")
                {
                    WriteGroupsToCSVFile(groups, writer);
                }
                else if (format == "xml")
                {
                    WriteGroupsToXMLFile(groups, contacts, typeData, writer);
                }
                else if (format == "json")
                {
                    WriteGroupsToJsonFile(groups, contacts, typeData, writer);
                }
                else
                {
                    Console.Out.Write("Unknown format" + format);
                }
                writer.Close();
            }
            
        }

        //Создаем excel файл из списка групп
        static void WriteGroupsToExcelFile(List<GroupData> groups, string filename)
        {
            //Учим запускать excel
            Excel.Application app = new Excel.Application();
            //app.Visible = true;
            
            //Создаем и добавлем новый док.
            Excel.Workbook wb = app.Workbooks.Add();
            
            //При создании wb, получаем новую страницу
            Excel.Worksheet sheet = wb.ActiveSheet;
            
            //Заполняем наш док инфой. Записываем в ячейки инфу из 
            int row = 1;
            foreach(GroupData group in groups)
            {
                sheet.Cells[row, 1] = group.GroupName;
                sheet.Cells[row, 2] = group.GroupHeader;
                sheet.Cells[row, 3] = group.GroupFooter;
                row++;
            }

            //Сохраняем наш док.
            //Строка содержит путь к файлу
            string fullPath = Path.Combine(Directory.GetCurrentDirectory(), filename);
            
            //Заранее удаляем файл по данному пути
            File.Delete(fullPath);
            
            //Сохраняем файл и закрыйваем
            wb.SaveAs(fullPath);
            wb.Close();

            //Закрыть просмотр
            //app.Visible = false;
            //Остановить процесс app
            app.Quit();
        }

        //Берем список с группами, записываем данные по каждой гркппе в файл csv
        static void WriteGroupsToCSVFile(List<GroupData> groups, StreamWriter writer)
        {
            foreach(GroupData group in groups)
            {
                writer.WriteLine(String.Format("${0},${1},${2}",
                    group.GroupName, group.GroupHeader, group.GroupFooter));
            }
        }

        //Создаем xml из списка групп или контактов
        static void WriteGroupsToXMLFile(List<GroupData> groups, List<EntryDate> contacts, string typeData, StreamWriter writer)
        {
            if (typeData == "groups")
            {
                new XmlSerializer(typeof(List<GroupData>)).Serialize(writer, groups);
            }
            if (typeData == "contacts")
            {
                new XmlSerializer(typeof(List<EntryDate>)).Serialize(writer, contacts);
            }
        }

        //Создаем json из списка групп или контактов
        static void WriteGroupsToJsonFile(List<GroupData> groups, List<EntryDate> contacts, string typeData, StreamWriter writer)
        {
            if (typeData == "groups")
            {
                writer.Write(JsonConvert.SerializeObject(groups, Newtonsoft.Json.Formatting.Indented));
            }
            if (typeData == "contacts")
            {
                writer.Write(JsonConvert.SerializeObject(contacts, Newtonsoft.Json.Formatting.Indented));
            }
        }
    }
}
