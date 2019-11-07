using System.Threading;
using NUnit.Framework;
using System.Collections.Generic;


namespace WebAddessbookTests
{
    [TestFixture]
    public class ContactInformationTests : AuhtTestsBase
    {
        [Test]
        public void TestContactInformation()
        {
            EntryDate fromTable =  AppManager.Contact.GetContactInformationFromTable(2);
            EntryDate fromForm = AppManager.Contact.GetContactInformationFromEditForm(2);

            Assert.AreEqual(fromTable, fromForm);
            Assert.AreEqual(fromTable.Address, fromForm.Address);
            Assert.AreEqual(fromTable.AllPhone, fromForm.AllPhone);
        }
    }
}
