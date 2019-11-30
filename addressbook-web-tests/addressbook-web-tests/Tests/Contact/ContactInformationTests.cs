using System.Threading;
using NUnit.Framework;
using System.Collections.Generic;


namespace WebAddessbookTests
{
    [TestFixture]
    public class ContactInformationTests : AuhtTestsBase
    {
        //Задание №11
        [Test]
        public void TestContactInformation_11task()
        {
            EntryDate fromTable =  AppManager.Contact.GetContactInformationFromTable(1);
            EntryDate fromForm = AppManager.Contact.GetContactInformationFromEditForm(1);

            Assert.AreEqual(fromTable, fromForm);
            Assert.AreEqual(fromTable.Address, fromForm.Address);
            Assert.AreEqual(fromTable.AllPhone, fromForm.AllPhone);
            Assert.AreEqual(fromTable.AllEmail, fromForm.AllEmail);
        }

        //Задание №12
        [Test]
        public void TestContactInformation_12task()
        {
            EntryDate fromForm = AppManager.Contact.GetContactInformationFromEditForm(7);
            EntryDate fromDetailForm = AppManager.Contact.GetContactInformationFromDetails(7);

            Assert.AreEqual(fromForm.FML, fromDetailForm.FML);
            Assert.AreEqual(fromForm.AllInfo, fromDetailForm.AllInfo);
            
        }
    }
}
