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
    public class ContactRemovalTests : AuhtTestsBase
    {
        //Читаем из json файла
        public static IEnumerable<EntryDate> ContactDataFromJsonFile()
        {
            return JsonConvert.DeserializeObject<List<EntryDate>>(File.ReadAllText(@"contacts.json"));
        }

        // 1 
        //Удаление с вкладки "home"
        //+ передаем в тест список из EntryDate, 
        [Test, TestCaseSource("ContactDataFromJsonFile")]
        public void ContactRemovalTestFromHome(EntryDate newEntry)
        {
            //EntryDate newEntry = new EntryDate("Ivan123", "Ivanov123", "Moscow, Pyshkina 3, room 123");
            //newEntry.MiddleName = "Ivanovich123";
            //newEntry.MobilePhone = "777777";
            //newEntry.E_mail = "Ivanov123@pochta.com";

            AppManager.Contact.CheckPresenceContact(newEntry);
            List<EntryDate> oldContactList = EntryDate.GetAll();
            
            //Сохраняем контакт,который будем удалять
            EntryDate oldContactForRemuval = oldContactList[0];

            AppManager.Contact.Removal(oldContactForRemuval);

            //Быстрая проверка
            Assert.AreEqual(oldContactList.Count - 1, AppManager.Contact.GetContactCount());

            List<EntryDate> newContactList = EntryDate.GetAll();
            oldContactList.RemoveAt(0);
                        
            AppManager.Contact.CheckContactResultByObj(oldContactList, newContactList);

            //Проверяем, что в новом списке контактов, нету с идентификитором удаленного.
            foreach (EntryDate c in newContactList)
            {
                Assert.AreNotEqual(c.Id, oldContactForRemuval.Id);
            }
        }

        
        // 2
        //Удаление через форму редактирования
        [Test, TestCaseSource("ContactDataFromJsonFile")]
        public void ContactRemovalTestFromEditForm(EntryDate newEntry)
        {
            //EntryDate newEntry = new EntryDate("Ivan123", "Ivanov123", "Moscow, Pyshkina 3, room 123");
            //newEntry.MiddleName = "Ivanovich123";
            //newEntry.MobilePhone = "777777";
            //newEntry.E_mail = "Ivanov123@pochta.com";

            AppManager.Contact.CheckPresenceContact(newEntry);
            List<EntryDate> oldContactList = EntryDate.GetAll();

            //Созраняем контакт,который будем удалять
            EntryDate removalContact = oldContactList[0];

            AppManager.Contact.Delete(removalContact);

            //Быстрая проверка
            Assert.AreEqual(oldContactList.Count - 1, AppManager.Contact.GetContactCount());

            List<EntryDate> newContactList = EntryDate.GetAll();
            oldContactList.RemoveAt(0);

            AppManager.Contact.CheckContactResultByObj(oldContactList, newContactList);

            //Проверяем, что в новом списке контактов, нету с идентификитором удаленного.
            foreach (EntryDate e in newContactList)
            {
                Assert.AreNotEqual(e.Id, removalContact.Id);
            }
        }
    }
}
