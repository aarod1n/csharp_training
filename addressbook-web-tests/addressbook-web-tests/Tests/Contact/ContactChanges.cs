using System.Threading;
using NUnit.Framework;

namespace WebAddessbookTests
{
    [TestFixture]
    public class ContactChanges : TestsBase
    {
        [Test]
        public void ContactChangeGroup()
        {
            AppManager.Contact.ChangeGroup(1);
            AppManager.Auth.logoff();
        }
    
        [Test]
        public void ContactChangeEntryData()
        {
            EntryDate entry = new EntryDate("Petrov", "Petr", "Moscow, Lenina 10, room 345");
            entry.MiddleName = "Petrovich";
            entry.Telephone = "123456789";
            entry.E_mail = "PPetrov@pochta.by";

            AppManager.Contact.Edit(1, entry);
            AppManager.Auth.logoff();
        }
    }
}
