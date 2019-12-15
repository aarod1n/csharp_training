using System.IO;
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
    public class ContactRemoveGroupTests : AuhtTestsBase
    {
        [Test]
        public void ContactRemoveGroup()
        {
            EntryDate contact = GroupData.GetAll()[0].GetContact().First();
            GroupData group = contact.GetGroup().First();
            List<EntryDate> oldListOfContacts = group.GetContact();
            

            AppManager.Contact.RemovFromGroup(contact, group);

            List<EntryDate> newListOfContacts = group.GetContact();
            oldListOfContacts.Remove(contact);
            oldListOfContacts.Sort();
            newListOfContacts.Sort();
            Assert.AreEqual(oldListOfContacts, newListOfContacts);



        }
    }
}
