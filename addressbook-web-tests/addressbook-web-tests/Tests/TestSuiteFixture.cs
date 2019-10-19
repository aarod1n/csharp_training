using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace WebAddessbookTests
{
    [SetUpFixture]
    public class TestSuiteFixture
    {
        public static ApplicationManager appManager;

        [SetUp]
        public void InitApplicationManager()
        {
            appManager = new ApplicationManager();

            appManager.Navigator.OpenStartPage();
            appManager.Auth.Login(new AccountData("admin", "secret"));
        }

        [TearDown]
        public void StopApplicationManager()
        {
            appManager.Quit();
        }
    }
}
