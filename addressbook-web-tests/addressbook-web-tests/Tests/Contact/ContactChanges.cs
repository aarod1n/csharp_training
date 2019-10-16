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
            EntryDate entry = new EntryDate("Petrov1", "Petr1", "Moscow, Lenina 101, room 3451");
            entry.MiddleName = "Petrovich";
            entry.Telephone = "123456789";
            entry.E_mail = "PPetrov@pochta.by";

            AppManager.Contact.Edit(-1, entry);
            AppManager.Auth.logoff();
        }
    }
}
