using System.Threading;
using NUnit.Framework;

namespace WebAddessbookTests
{
    [TestFixture]
    public class ContactRemovalTests : TestsBase
    {
        //Удаление с вкладки "home"
        [Test]
        public void ContactRemovalTestFromHome()
        {
            AppManager.Contact.Removal(1); 
            AppManager.Auth.logoff();
        }

        //Удаление через форму редактирования
        [Test]
        public void ContactRemovalTestFromEditForm()
        {
            AppManager.Contact.Delete(1);
            AppManager.Auth.logoff();
        }
    }
}
