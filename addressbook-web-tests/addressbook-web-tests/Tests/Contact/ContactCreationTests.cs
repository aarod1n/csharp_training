using System.Threading;
using NUnit.Framework;
using System.Collections.Generic;


namespace WebAddessbookTests
{
    [TestFixture]
    public class ContactCreationTests : AuhtTestsBase
    {  
        [Test]
        public void ContactCreationTest()
        {
            List<EntryDate> oldContactsList = AppManager.Contact.GetContactList();
            int count = 0;
            for (int i = 0; i < 3; i++)
            {
                EntryDate entry = new EntryDate("Ivan" + i, "Ivanov" + i, "Moscow, Pyshkina 3, room 1");
                entry.MiddleName = "Ivanovich" + i;
                entry.Telephone = "777777" + i;
                entry.E_mail = "Ivanov" + i + "@pochta.com";

                AppManager.Contact.Create(entry);
                count++;
            }
            
            Assert.AreEqual(oldContactsList.Count + count, AppManager.Contact.GetContactCount());

            List<EntryDate> NewContactsList = AppManager.Contact.GetContactList();
            
            for (int i = 0; i < count; i++)
            {
                oldContactsList.Add(new EntryDate("Ivan" + i, "Ivanov" + i, "Moscow, Pyshkina 3, room 1"));                
            }            
            AppManager.Contact.CheckContactResultByObj(oldContactsList, NewContactsList);
        }

        //Тест будет падать, так как есть баг на форме
        //Добавление быстрой проверки сократило тест на 2 секунды
        [Test]
        public void ContactCreationInvalidNameTest()
        {
            List<EntryDate> oldContactsList = AppManager.Contact.GetContactList();
            int count = 0;
            for (int i = 0; i < 3; i++)
            {
                EntryDate entry = new EntryDate("Ivan'" + i, "Ivanov" + i, "Moscow, Pyshkina 3, room 1");
                entry.MiddleName = "Ivanovich" + i;
                entry.Telephone = "777777" + i;
                entry.E_mail = "Ivanov" + i + "@pochta.com";

                AppManager.Contact.Create(entry);
                count++;
            }

            Assert.AreEqual(oldContactsList.Count + count, AppManager.Contact.GetContactCount());

            List<EntryDate> NewContactsList = AppManager.Contact.GetContactList();

            for (int i = 0; i < count; i++)
            {
                oldContactsList.Add(new EntryDate("Ivan'" + i, "Ivanov" + i, "Moscow, Pyshkina 3, room 1"));

            }            
            AppManager.Contact.CheckContactResultByObj(oldContactsList, NewContactsList);
        }
    }
}
