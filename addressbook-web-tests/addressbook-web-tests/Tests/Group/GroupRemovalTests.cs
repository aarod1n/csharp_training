using System.Threading;
using NUnit.Framework;


namespace WebAddessbookTests
{
    [TestFixture]
    public class GroupRemovalTests : AuhtTestsBase
    {

        [Test]
        public void GroupRemovalTest()
        {
            GroupData group = new GroupData("test123Removoalname");
            group.GroupHeader = "test123Removoalheader";
            group.GroupFooter = "test123Removoalfooter";            
            
            AppManager.Group.CheckPresenceGroup(group);
            AppManager.Group.RemovaGroup(1);
        }
    }
}