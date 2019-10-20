using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace WebAddessbookTests
{
    public class AuhtTestsBase : TestsBase
    {
        [SetUp]
        public void SetupLogin()
        {
            AppManager.Auth.Login(new AccountData("admin", "secret"));
        }
    }
}
