using System.Threading;
using NUnit.Framework;
using System.Collections.Generic;

namespace WebAddessbookTests
{
    [TestFixture]
    public class ContactRemovalTests : AuhtTestsBase
    {
        //Удаление с вкладки "home"
        [Test]
        public void ContactRemovalTestFromHome()
        {
            EntryDate newEntry = new EntryDate("Ivan123", "Ivanov123", "Moscow, Pyshkina 3, room 123");
            newEntry.MiddleName = "Ivanovich123";
            newEntry.Telephone = "777777";
            newEntry.E_mail = "Ivanov123@pochta.com";

            AppManager.Contact.CheckPresenceContact(newEntry);
            List<EntryDate> oldContactList = AppManager.Contact.GetContactList();
            
            //Созраняем контакт,который будем удалять
            EntryDate oldContact = oldContactList[1];

            AppManager.Contact.Removal(1);

            //Быстрая проверка
            Assert.AreEqual(oldContactList.Count - 1, AppManager.Contact.GetContactCount());

            List<EntryDate> newContactList = AppManager.Contact.GetContactList();
            oldContactList.RemoveAt(1);
            
            AppManager.Contact.CheckContactResultByObj(oldContactList, newContactList);

            //Проверяем, что в новом списке контактов, нету с идентификитором удаленного.
            foreach (EntryDate c in newContactList)
            {
                Assert.AreNotEqual(c.Id, oldContact.Id);
            }
        }

        //Удаление через форму редактирования
        [Test]
        public void ContactRemovalTestFromEditForm()
        {
            EntryDate newEntry = new EntryDate("Ivan123", "Ivanov123", "Moscow, Pyshkina 3, room 123");
            newEntry.MiddleName = "Ivanovich123";
            newEntry.Telephone = "777777";
            newEntry.E_mail = "Ivanov123@pochta.com";

            AppManager.Contact.CheckPresenceContact(newEntry);
            List<EntryDate> oldContactList = AppManager.Contact.GetContactList();

            //Созраняем контакт,который будем удалять
            EntryDate removalContact = oldContactList[1];

            AppManager.Contact.Delete(1);

            //Быстрая проверка
            Assert.AreEqual(oldContactList.Count - 1, AppManager.Contact.GetContactCount());

            List<EntryDate> newContactList = AppManager.Contact.GetContactList();
            oldContactList.RemoveAt(1);

            AppManager.Contact.CheckContactResultByObj(oldContactList, newContactList);

            //Проверяем, что в новом списке контактов, нету с идентификитором удаленного.
            foreach (EntryDate e in newContactList)
            {
                Assert.AreNotEqual(e.Id, removalContact.Id);
            }
        }
    }
}
