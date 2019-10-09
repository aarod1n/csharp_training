using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;


namespace WebAddessbookTests
{
    public class ContactHelper : BaseHelper
    {
        public ContactHelper(IWebDriver driver) : base(driver) 
        {
        }

        /// <summary>
        /// Методы для тестов создания контактов
        /// </summary>
        public void GoToAddNewEntry() => driver.FindElement(By.LinkText("add new")).Click();

        public void FillEntryForm(EntryDate entry)
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

        public void SubmitNewEntry()
        {
            driver.FindElement(By.Name("submit")).Click();
        }

    }
}
