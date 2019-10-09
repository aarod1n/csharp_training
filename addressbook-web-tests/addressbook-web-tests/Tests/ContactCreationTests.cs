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
            AppManager.Navigator.OpenStartPage();
            AccountData user = new AccountData("admin", "secret");
            AppManager.Auth.Login(user);
            AppManager.Contact.GoToAddNewEntry();
            EntryDate entry = new EntryDate("Ivan", "Ivanov", "Moscow, Pyshkina 3, room 1");
            entry.MiddleName = "Ivanovich";
            entry.Telephone = "777777";
            entry.E_mail = "Ivanov@pochta.com";
            AppManager.Contact.FillEntryForm(entry);
            AppManager.Contact.SubmitNewEntry();
            AppManager.Navigator.GoToHome();
            Thread.Sleep(5000);
            AppManager.Auth.logoff();
        }
    }
}
