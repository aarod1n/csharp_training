using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace WebAddessbookTests
{
    public class TestsBase
    {
        protected IWebDriver driver;
        protected StringBuilder verificationErrors;
        protected string baseURL;
        protected bool acceptNextAlert = true;

        [SetUp]
        public void SetupTest()
        {
            //OpenQA.Selenium.Firefox.FirefoxDriver
            driver = new ChromeDriver();
            baseURL = "http://localhost/addressbook";
            verificationErrors = new StringBuilder();
        }

        //Метод логина в БД
        protected void Login(AccountData user)
        {
            driver.FindElement(By.Name("user")).Click();
            driver.FindElement(By.Name("user")).Clear();
            driver.FindElement(By.Name("user")).SendKeys(user.UserName);
            driver.FindElement(By.Name("pass")).Clear();
            driver.FindElement(By.Name("pass")).SendKeys(user.Password);
            driver.FindElement(By.CssSelector("input[type=\"submit\"]")).Click();
        }

        protected void OpenStartPage()
        {
            driver.Navigate().GoToUrl(baseURL);
        }

        protected void GoToHome()
        {
            driver.FindElement(By.LinkText("home")).Click();
        }

        //Метод для logoff'a
        protected void ReturnStartPage()
        {
            driver.FindElement(By.LinkText("Logout")).Click();
        }

        /// <summary>
        /// Методы для тестов групп
        /// </summary>
        protected void GoToGroupPage()
        {
            driver.FindElement(By.LinkText("groups")).Click();
        }
        protected void CreatedGroup()
        {
            driver.FindElement(By.Name("submit")).Click();
        }
        protected void InitNewGroupCreation()
        {
            driver.FindElement(By.Name("new")).Click();
        }
        protected void FillGroupForm(GroupData group)
        {
            driver.FindElement(By.Name("group_name")).Clear();
            driver.FindElement(By.Name("group_name")).SendKeys(group.GroupName);
            driver.FindElement(By.Name("group_header")).Clear();
            driver.FindElement(By.Name("group_header")).SendKeys(group.GroupHeader);
            driver.FindElement(By.Name("group_footer")).Clear();
            driver.FindElement(By.Name("group_footer")).SendKeys(group.GroupFooter);
        }
        protected void SubmitGroupCreation()
        {
            driver.FindElement(By.LinkText("groups")).Click();
        }

        protected void SelectGroup(int index)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])["+ index +"]")).Click();
        }
        
        protected void DeleteGroup()
        {
            driver.FindElement(By.Name("delete")).Click();
        }

        /// <summary>
        /// Методы для тестов создания контактов
        /// </summary>
        protected void GoToAddNewEntry() => driver.FindElement(By.LinkText("add new")).Click();

        protected void FillEntryForm(EntryDate entry)
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
            List<IWebElement> options = driver.FindElement(By.Name("new_group")).FindElements(By.TagName("option")).ToList();
            for (int i = 0; i < options.Count; i++)
            {
                string element;
                element = options[i].GetAttribute("value");
                if (!element.Equals("none"))
                    options[i].Click();
            }
        }

        protected void SubmitNewEntry()
        {
            driver.FindElement(By.Name("submit")).Click();
        }


    }
}
