using System.Threading;
using NUnit.Framework;


namespace WebAddessbookTests
{
    [TestFixture]
    public class ContactCreationTests : AuhtTestsBase
    {  
        [Test]
        public void ContactCreationTest()
        {
            EntryDate entry = new EntryDate("Ivan", "Ivanov", "Moscow, Pyshkina 3, room 1");
            entry.MiddleName = "Ivanovich";
            entry.Telephone = "777777";
            entry.E_mail = "Ivanov@pochta.com";

            AppManager.Contact.Create(entry);            
        }
    }
}
