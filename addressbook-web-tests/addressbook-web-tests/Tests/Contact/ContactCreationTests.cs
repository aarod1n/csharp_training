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
    public class ContactCreationTests : AuhtTestsBase
    {
        //Создаем список с объектами EntryDate у которых случайным образом заполняются свойства.
        //public static IEnumerable<EntryDate> RandomContactDataProvider()
        //{
        //    List<EntryDate> entry = new List<EntryDate>();

        //    for (int i = 0; i < 3; i++)
        //    {
        //        entry.Add(new EntryDate()
        //        {
        //            FirstName = GenerationRandomString(10),
        //            MiddleName = GenerationRandomString(10),
        //            LastName = GenerationRandomString(10),
        //            Address = GenerationRandomString(10),
        //            MobilePhone = GenerationRandomString(10),
        //            E_mail = GenerationRandomString(10)
        //        });
        //    }
        //    return entry;
        //}

        //Читаем из xml файла
        public static IEnumerable<EntryDate> ContactDataFromXmlFile()
        {
            return (List<EntryDate>)
                new XmlSerializer(typeof(List<EntryDate>)).Deserialize(new StreamReader(@"contacts.xml"));
        }

        //Читаем из json файла
        public static IEnumerable<EntryDate> ContactDataFromJsonFile()
        {
            return JsonConvert.DeserializeObject<List<EntryDate>>(File.ReadAllText(@"contacts.json"));
        }


        [Test, TestCaseSource("ContactDataFromJsonFile")]
        public void ContactCreationTest(EntryDate entry)
        {
            List<EntryDate> oldContactsList = EntryDate.GetAll();
            AppManager.Contact.Create(entry);
            Assert.AreEqual(oldContactsList.Count + 1, AppManager.Contact.GetContactCount());
            oldContactsList.Add(entry);
            List<EntryDate> NewContactsList = EntryDate.GetAll();
            AppManager.Contact.CheckContactResultByObj(oldContactsList, NewContactsList);
        }
    }
}
