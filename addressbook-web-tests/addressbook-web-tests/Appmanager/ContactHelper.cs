using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;


namespace WebAddessbookTests
{
    public class ContactHelper : BaseHelper
    {
        public ContactHelper(ApplicationManager manager) : base(manager)
        {
        }

        //2 lvl 
        public ContactHelper Create(EntryDate entry)
        {
            GoToAddNewEntry();
            FillEntryForm(entry);
            SubmitNewEntry();
            manager.Navigator.GoToHome();
            return this;
        }

        public ContactHelper ChangeGroup(int v)
        {
            manager.Navigator.GoToHome();
            SelectContact(v);
            SelectGroupAddTo();
            AddToGroupClick();
            manager.Navigator.GoToHome();
            return this;
        }

        public ContactHelper RemovalContact(int v)
        {
            manager.Navigator.GoToHome();
            SelectContact(1);
            DeleteContact();
            ClosedAlert();
            manager.Navigator.GoToHome();
            return this;
        }

        //1 lvl 
        public ContactHelper GoToAddNewEntry() 
        { 
            driver.FindElement(By.LinkText("add new")).Click(); 
            return this; 
        }

        public ContactHelper FillEntryForm(EntryDate entry)
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
                if (!element.Equals("[none]"))
                    options[i].Click();
            }

            return this;
        }

        public ContactHelper SubmitNewEntry()
        {
            driver.FindElement(By.Name("submit")).Click();
            return this;
        }

        public ContactHelper SelectContact(int index)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + index + "]")).Click();
            return this;
        }

        public ContactHelper DeleteContact()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            return this;
        }

        public ContactHelper ClosedAlert()
        {
            driver.SwitchTo().Alert().Accept();
            return this;
        }

        //Для отображения контактов в конкретной группе(тестовый способ, можно настроить)
        public ContactHelper SelectGroupView()
        {
            List<IWebElement> options = driver.FindElement(By.Name("group")).FindElements(By.TagName("option")).ToList();
            for (int i = 0; i < options.Count; i++)
            {
                string element;
                element = options[i].GetAttribute("value");
                if (!element.Equals("[none]") & !element.Equals("test2name"))
                    options[i].Click();
            }
            return this; 
        }

        //Оказалось из строчки <option value="38">test2name</option> в переменную element пишем "38", что является id test2name.
        //Как передать значение "test2name" и сравнивать с ним, пока не разобрался.
        //В варианте для создания контакта строчка "<option value="[none]">[none]</option>" удачно совпала.
        public ContactHelper SelectGroupAddTo()
        {
            List<IWebElement> options = driver.FindElement(By.Name("to_group")).FindElements(By.TagName("option")).ToList();
            for (int i = 0; i < options.Count; i++)
            {
                string element;
                element = options[i].GetAttribute("value");
                if(!element.Equals("38")) 
                {
                    options[i].Click();
                }                    
            }
            return this;
        }

        public ContactHelper AddToGroupClick()
        {
            driver.FindElement(By.Name("add")).Click();
            return this;
        }
    }
}
