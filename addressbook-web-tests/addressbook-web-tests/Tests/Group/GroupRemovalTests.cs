using System.Threading;
using NUnit.Framework;


namespace WebAddessbookTests
{
    [TestFixture]
    public class GroupRemovalTests : TestsBase
    {

        [Test]
        public void GroupRemovalTest()
        {
            AppManager.Navigator.OpenStartPage();
            AppManager.Group.RemovaGroup(1);
        }
    }
}