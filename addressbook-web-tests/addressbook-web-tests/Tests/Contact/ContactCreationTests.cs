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
                    FirstName = GenerationRandomString(10, false),
                    MiddleName = GenerationRandomString(10, false),
                    LastName = GenerationRandomString(10, false),
                    Address = GenerationRandomString(30, false),
                    MobilePhone = GenerationRandomString(10, true),
                    E_mail = GenerationRandomString(10, false)
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
