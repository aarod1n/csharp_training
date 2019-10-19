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
        [SetUp]
        public void InitApplicationManager()
        {
            ApplicationManager appManager = ApplicationManager.GetInstance();

            appManager.Navigator.OpenStartPage();
            appManager.Auth.Login(new AccountData("admin", "secret"));
        }
    }
}
