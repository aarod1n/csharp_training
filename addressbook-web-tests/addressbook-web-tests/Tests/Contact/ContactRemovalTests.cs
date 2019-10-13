using System.Threading;
using NUnit.Framework;

namespace WebAddessbookTests
{
    [TestFixture]
    public class ContactRemovalTests : TestsBase
    {
        [Test]
        public void ContactRemovalTestsTest()
        {
            AppManager.Contact.RemovalContact(1);
            AppManager.Auth.logoff();
        }
    }
}
