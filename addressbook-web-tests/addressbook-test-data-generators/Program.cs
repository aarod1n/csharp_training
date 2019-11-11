using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAddessbookTests;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace addressbook_test_data_generators
{
    class Program
    {
        static void Main(string[] args)
        {
            int count = Convert.ToInt32(args[0]); 
            //Открываем на запись
            StreamWriter writer = new StreamWriter(args[1]);
            //Передаем формат файла
            string format = args[2];
            
            //Список для формирования сгенеренных данных
            List<GroupData> groups = new List<GroupData>();
            //В цикле наполняем список данными, группа = одна строка
            for (int i = 0; i < count; i++)
            {

                groups.Add(new GroupData(TestsBase.GenerationRandomString(10))
                {
                    GroupHeader = TestsBase.GenerationRandomString(10),
                    GroupFooter = TestsBase.GenerationRandomString(10)
                });
            }
            if (format == "csv")
            {
                WriteGroupsToCSVFile(groups, writer);
            }
            else if (format == "xml")
            {
                WriteGroupsToXMLFile(groups, writer);
            }
            else
            {
                Console.Out.Write("Unknown format" + format);
            }
            writer.Close();
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

        //Создаем xml из списка групп
        static void WriteGroupsToXMLFile(List<GroupData> groups, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<GroupData>)).Serialize(writer, groups);
        }
    }
}
