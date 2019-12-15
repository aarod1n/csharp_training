using System.Threading;
using NUnit.Framework;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Excel = Microsoft.Office.Interop.Excel;
using System.Linq;

namespace WebAddessbookTests
{
    [TestFixture]
    public class ContactAddGroupTests : AuhtTestsBase
    {
        [Test]
        public void ContactAddGroup()
        {
            GroupData group = GroupData.GetAll()[0];
            List<EntryDate> oldListOfContacts = group.GetContact();
            EntryDate entry = EntryDate.GetAll().Except(oldListOfContacts).First();

            AppManager.Contact.AddInGroup(entry, group);

            List<EntryDate> newListOfContacts = group.GetContact();
            oldListOfContacts.Add(entry);
            oldListOfContacts.Sort();
            newListOfContacts.Sort();
            Assert.AreEqual(oldListOfContacts, newListOfContacts);
            

            
        }
    }
}
