using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace WebAddessbookTests
{
    [TestFixture]
    public class ContactCreationTests
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;

        [SetUp]
        public void SetupTest()
        {
            driver = new ChromeDriver();
            baseURL = "http://localhost/addressbook";
            verificationErrors = new StringBuilder();
        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }

        [Test]
        public void ContactCreationTest()
        {
            OpenStartPage();
            AccountData user = new AccountData("admin", "secret");
            Login(user);
            GoToAddNewEntry();
            EntryDate entry = new EntryDate("Ivan", "Ivanov", "Moscow, Pyshkina 3, room 1");
            entry.MiddleName = "Ivanovich";
            entry.Telephone = "777777";
            entry.E_mail = "Ivanov@pochta.com";
            FillEntryForm(entry);
            SubmitNewEntry();
            GoToHome();
            Thread.Sleep(5000);
        }

        //Написанные методы
        private void ReturnStartPage()
        {
            driver.FindElement(By.LinkText("Logout")).Click();
        }

        private void GoToHome()
        {
            driver.FindElement(By.LinkText("home")).Click();
        }

        private void FillEntryForm(EntryDate entry)
        {
            driver.FindElement(By.Name("firstname")).Clear();
            driver.FindElement(By.Name("firstname")).SendKeys(entry.FirstName);
            driver.FindElement(By.Name("lastname")).Clear();
            driver.FindElement(By.Name("lastname")).SendKeys(entry.LastName);
            driver.FindElement(By.Name("address")).Clear();
            driver.FindElement(By.Name("address")).SendKeys(entry.Address);
            driver.FindElement(By.Name("middlename")).Clear();
            driver.FindElement(By.Name("middlename")).SendKeys(entry.MiddleName);
            driver.FindElement(By.Name("mobile")).Clear();
            driver.FindElement(By.Name("mobile")).SendKeys(entry.Telephone);
            driver.FindElement(By.Name("email")).Clear();
            driver.FindElement(By.Name("email")).SendKeys(entry.E_mail);


            //Выбор значения из выпадающего списка не равного "none"
            List <IWebElement> options = driver.FindElement(By.Name("new_group")).FindElements(By.TagName("option")).ToList();
            for(int i = 0; i < options.Count; i++)
            {
                string element;
                element = options[i].GetAttribute("value");
                if (!element.Equals("none")) 
                    options[i].Click();
            }            
        }

        private void GoToAddNewEntry() => driver.FindElement(By.LinkText("add new")).Click();

        private void Login(AccountData user)
        {
            driver.FindElement(By.Name("user")).Click();
            driver.FindElement(By.Name("user")).Clear();
            driver.FindElement(By.Name("user")).SendKeys(user.UserName);
            driver.FindElement(By.Name("pass")).Clear();
            driver.FindElement(By.Name("pass")).SendKeys(user.Password);
            driver.FindElement(By.CssSelector("input[type=\"submit\"]")).Click();
        }

        private void OpenStartPage()
        {
            driver.Navigate().GoToUrl(baseURL);
        }

        private void SubmitNewEntry()
        {
            driver.FindElement(By.Name("submit")).Click();
        }

        //Автогенерация
        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        private bool IsAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }

        private string CloseAlertAndGetItsText()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                return alertText;
            }
            finally
            {
                acceptNextAlert = true;
            }
        }
    }
}
