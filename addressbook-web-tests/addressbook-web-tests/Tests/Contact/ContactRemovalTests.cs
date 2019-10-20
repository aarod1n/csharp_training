using System.Threading;
using NUnit.Framework;

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
            AppManager.Contact.Removal(1);           
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
            AppManager.Contact.Delete(1);            
        }
    }
}
