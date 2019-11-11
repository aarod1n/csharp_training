using System.Threading;
using NUnit.Framework;
using System.Collections.Generic;


namespace WebAddessbookTests
{
    [TestFixture]
    public class ContactCreationTests : AuhtTestsBase
    {
        //Создаем список с объектами EntryDate у которых случайным образом заполняются свойства.
        public static IEnumerable<EntryDate> RondomContactDataProvider()
        {
            List<EntryDate> entry = new List<EntryDate>();

            for (int i = 0; i < 3; i++)
            {
                entry.Add(new EntryDate()
                {
                    FirstName = GenerationRandomString(10),
                    MiddleName = GenerationRandomString(10),
                    LastName = GenerationRandomString(10),
                    Address = GenerationRandomString(10),
                    MobilePhone = GenerationRandomString(10),
                    E_mail = GenerationRandomString(10)
                });
            }
            return entry;
        }


        [Test, TestCaseSource("RondomContactDataProvider")]
        public void ContactCreationTest(EntryDate entry)
        {
            List<EntryDate> oldContactsList = AppManager.Contact.GetContactList();
            AppManager.Contact.Create(entry);
            Assert.AreEqual(oldContactsList.Count + 1, AppManager.Contact.GetContactCount());
            oldContactsList.Add(entry);
            List<EntryDate> NewContactsList = AppManager.Contact.GetContactList();
            AppManager.Contact.CheckContactResultByObj(oldContactsList, NewContactsList);
        }
    }
}
