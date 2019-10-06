using System.Threading;
using NUnit.Framework;


namespace WebAddessbookTests
{
    [TestFixture]
    public class ContactCreationTests : TestsBase
    {  
        [Test]
        public void ContactCreationTest()
        {
            OpenStartPage();
            AccountData user = new AccountData("admin", "secret");
            Login(user);
            GoToAddNewEntry();
            EntryDate entry = new EntryDate("Ivan", "Ivanov", "Moscow, Pyshkina 3, room 1");
            entry.MiddleName = "Ivanovich";
            entry.Telephone = "777777";
            entry.E_mail = "Ivanov@pochta.com";
            FillEntryForm(entry);
            SubmitNewEntry();
            GoToHome();
            Thread.Sleep(5000);
        }
    }
}
