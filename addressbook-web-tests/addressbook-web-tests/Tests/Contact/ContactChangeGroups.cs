using System.Threading;
using NUnit.Framework;

namespace WebAddessbookTests
{
    [TestFixture]
    public class ContactChangeGroups : TestsBase
    {
        [Test]
        public void ContactChangeGroup()
        {
            AppManager.Contact.ChangeGroup(1);
            AppManager.Auth.logoff();
        }
    }
}
