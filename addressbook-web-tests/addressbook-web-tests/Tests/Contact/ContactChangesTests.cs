using System.Threading;
using NUnit.Framework;
using System.Collections.Generic;

namespace WebAddessbookTests
{
    [TestFixture]
    public class ContactChangesTests : AuhtTestsBase
    {
        [Test]
        public void ContactChangeGroup()
        {
            EntryDate newEntry = new EntryDate("Ivan123", "Ivanov123", "Moscow, Pyshkina 3, room 123");
            newEntry.MiddleName = "Ivanovich123";
            newEntry.Telephone = "777777";
            newEntry.E_mail = "Ivanov123@pochta.com";

            AppManager.Contact.CheckPresenceContact(newEntry);
            AppManager.Contact.ChangeGroup(1);
        }
    
        [Test]
        public void ContactChangeEntryData()
        {
            EntryDate newEntry = new EntryDate("Ivan123", "Ivanov123", "Moscow, Pyshkina 3, room 123");
            newEntry.MiddleName = "Ivanovich123";
            newEntry.Telephone = "777777";
            newEntry.E_mail = "Ivanov123@pochta.com";

            AppManager.Contact.CheckPresenceContact(newEntry);
            List<EntryDate> oldContactList = AppManager.Contact.GetContactList();

            //Созраняем контакт,который будем изменять
            EntryDate oldContact = oldContactList[0];

            EntryDate changeEntry = new EntryDate("Petr1", "Petrov1", "Moscow, Lenina 101, room 3451");
            changeEntry.MiddleName = "Petrovich";
            changeEntry.Telephone = "123456789";
            changeEntry.E_mail = "PPetrov@pochta.by";

            AppManager.Contact.Edit(0, changeEntry);

            //Быстрая проверка
            Assert.AreEqual(oldContactList.Count, AppManager.Contact.GetContactCount());

            List<EntryDate> newContactList = AppManager.Contact.GetContactList();
            AppManager.Contact.CheckContactChangeResultByObj(oldContactList, newContactList, changeEntry, 0);

            //Проверяем изменение имени и фамилии по нашему Id
            foreach (EntryDate e in newContactList)
            {
                if (e.Id == oldContact.Id)
                {
                    Assert.AreEqual(changeEntry.FirstName, e.FirstName);
                    Assert.AreEqual(changeEntry.LastName, e.LastName);
                }
            }
        }
    }
}
