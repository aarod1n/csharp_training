using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Text.RegularExpressions;
using OpenQA.Selenium.Support.UI;

namespace WebAddessbookTests
{
    public class ContactHelper : BaseHelper
    {
        private List<EntryDate> contactCash = null;

        public ContactHelper(ApplicationManager manager) : base(manager)
        {
        }

        //2 lvl
        ///////////////////////////////////////////////////////////////////////////
        public ContactHelper Create(EntryDate entry)
        {
            manager.Navigator.OpenStartPage();
            GoToAddNewEntry();
            FillEntryForm(entry);
            SubmitNewEntry();
            manager.Navigator.GoToHome();
            return this;
        }

        public ContactHelper Removal(int index)
        {
            manager.Navigator.OpenStartPage();
            manager.Navigator.GoToHome();
            SelectContact(index);
            DeleteContact();
            ClosedAlert();
            manager.Navigator.GoToHome();
            return this;
        }

        public ContactHelper Removal(EntryDate oldContactForRemuval)
        {
            manager.Navigator.OpenStartPage();
            manager.Navigator.GoToHome();
            SelectContact(oldContactForRemuval.Id);
            DeleteContact();
            ClosedAlert();
            manager.Navigator.GoToHome();
            return this;
        }

        public ContactHelper Edit(int index, EntryDate entry)
        {
            manager.Navigator.OpenStartPage();
            manager.Navigator.GoToHome();
            SelectContactChange(index + 1);
            FillEntryForm(entry);
            SubmitUpdateEntry();
            manager.Navigator.GoToHome();
            return this;
        }

        public ContactHelper Edit(string id, EntryDate changeEntry)
        {
            manager.Navigator.OpenStartPage();
            manager.Navigator.GoToHome();
            SelectContactChange(id);
            FillEntryForm(changeEntry);
            SubmitUpdateEntry();
            manager.Navigator.GoToHome();
            return this;
        }

        public ContactHelper Delete(int index)
        {
            manager.Navigator.OpenStartPage();
            manager.Navigator.GoToHome();
            SelectContactChange(index);
            SubmitDeleteEntry();
            manager.Navigator.GoToHome();
            return this;
        }

        public ContactHelper Delete(EntryDate removalContact)
        {
            manager.Navigator.OpenStartPage();
            manager.Navigator.GoToHome();
            SelectContactChange(removalContact.Id);
            SubmitDeleteEntry();
            manager.Navigator.GoToHome();
            return this;
        }

        public ContactHelper AddInGroup(EntryDate contact, GroupData group)
        {
            manager.Navigator.OpenStartPage();
            manager.Navigator.GoToHome();
            ClearGroupFilter();
            SelectContact(contact.Id);
            SelectGroupToAdd(group.GroupName);
            AddToGroupClick();

            //Ожидаем появления на страницы элемента By.CssSelector("#content > div"), хотя бы одного 
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElements(By.CssSelector("#content > div")).Count > 0);

            return this;
        }

        public ContactHelper RemovFromGroup(EntryDate contact, GroupData group)
        {
            manager.Navigator.OpenStartPage();
            manager.Navigator.GoToHome();
            SelectGroupView(group.GroupName);
            SelectContact(contact.Id);
            RemoveToGroupClick();
            manager.Navigator.GoToHome();
            return this;
        }

        //Проверка наличия контакта
        public ContactHelper CheckPresenceContact(EntryDate entry)
        {
            manager.Navigator.GoToHome();
            if (!(IsElementPresent(By.CssSelector("a[href^='edit.php?']"))))
            {
                manager.Contact.Create(entry);
            }
            return this;
        }

        //Возвращаем кол-во элементов контактов для быстрой проверки
        public int GetContactCount()
        {
            return driver.FindElements(By.CssSelector("tr[name=entry]")).Count;
        }

        //Получение всех созданных контактов, метод реализован с помощью кеширования
        public List<EntryDate> GetContactList()
        {
            if (contactCash == null)
            {
                contactCash = new List<EntryDate>();
                manager.Navigator.GoToHome();

                //Получаем кол-во строк с контактами
                ICollection<IWebElement> elements = driver.FindElements(By.CssSelector("tr[name=entry]"));

                //Берем строку и парсим ее на ячейки, берем нужное значение ячейки.
                foreach (IWebElement row in elements)
                {
                    List<IWebElement> cell = row.FindElements(By.TagName("td")).ToList();
                    //Добавляем в кеш лист новый контакт
                    contactCash.Add(new EntryDate(cell[2].Text, cell[1].Text)
                    {
                        //Сразу присваеваем свойству Id значение атрибута
                        Id = row.FindElement(By.TagName("input")).GetAttribute("id")
                    });
                }
            }
            return new List<EntryDate>(contactCash);
        }

        //Проверка по кло-ву элементов в списке
        public void CheckContactResultByCount(List<EntryDate> oldContactsList, List<EntryDate> newContactsList)
        {
            Assert.AreEqual(oldContactsList.Count, newContactsList.Count);
        }

        //Проверка элементов путем сравнения самих объектов
        public void CheckContactResultByObj(List<EntryDate> oldContactsList, List<EntryDate> newContactsList)
        {
            oldContactsList.Sort();
            newContactsList.Sort();
            Assert.AreEqual(oldContactsList, newContactsList);
        }
        public void CheckContactChangeResultByObj(List<EntryDate> oldContactsList, List<EntryDate> newContactsList, EntryDate entry, int index)
        {
            oldContactsList[index].FirstName = entry.FirstName;
            oldContactsList[index].LastName = entry.LastName;
            oldContactsList.Sort();
            newContactsList.Sort();
            Assert.AreEqual(oldContactsList, newContactsList);
        }

        public void CheckContactChangeResultByObj(EntryDate oldContact, EntryDate changeEntry, List<EntryDate> oldContactList, List<EntryDate> newContactList)
        {
            for (int i = 0; i < oldContactList.Count; i++)
            {
                if (oldContactList[i].Id == oldContact.Id)
                {
                    oldContactList[i].FirstName = changeEntry.FirstName;
                    oldContactList[i].MiddleName = changeEntry.MiddleName;
                    oldContactList[i].LastName = changeEntry.LastName;
                    oldContactList[i].Address = changeEntry.Address;
                    oldContactList[i].MobilePhone = changeEntry.MobilePhone;
                    oldContactList[i].E_mail = changeEntry.E_mail;
                }
            }
            oldContactList.Sort();
            newContactList.Sort();
            Assert.AreEqual(oldContactList, newContactList);
        }

        //Получаем элементы из таблицы, парсим по нужным полям.
        public EntryDate GetContactInformationFromTable(int index)
        {
            manager.Navigator.GoToHome();
            IList<IWebElement> cell = driver.FindElements(By.CssSelector("tr[name=entry]"))[index - 1].FindElements(By.TagName("td"));

            string lastname = cell[1].Text;
            string firstname = cell[2].Text;
            string address = cell[3].Text;
            string email = cell[4].Text;
            string allPhone = cell[5].Text;

            return new EntryDate(firstname, lastname, address)
            {
                AllPhone = allPhone,
                E_mail = email
            };
        }

        //Получаем элементы с формы, парсим по нужным полям.
        public EntryDate GetContactInformationFromEditForm(int index)
        {
            manager.Navigator.GoToHome();
            manager.Contact.SelectContactChange(index);

            string firstname = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string middlename = driver.FindElement(By.Name("middlename")).GetAttribute("value");
            string lastname = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string email = driver.FindElement(By.Name("email")).GetAttribute("value");
            string email2 = driver.FindElement(By.Name("email2")).GetAttribute("value");
            string email3 = driver.FindElement(By.Name("email3")).GetAttribute("value");
            string nickname = driver.FindElement(By.Name("nickname")).GetAttribute("value");
            string company = driver.FindElement(By.Name("company")).GetAttribute("value");
            string title = driver.FindElement(By.Name("title")).GetAttribute("value");
            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");
            string fax = driver.FindElement(By.Name("fax")).GetAttribute("value");
            string homepage = driver.FindElement(By.Name("homepage")).GetAttribute("value");
            string secondaryHomePhone = driver.FindElement(By.Name("phone2")).GetAttribute("value");
            string notes = driver.FindElement(By.Name("notes")).GetAttribute("value");
            string secondaryAddress = driver.FindElement(By.Name("address2")).GetAttribute("value");

            return new EntryDate(firstname, lastname)
            {
                MiddleName = middlename,
                Address = address,
                MobilePhone = mobilePhone,
                HomePhone = homePhone,
                WorkPhone = workPhone,
                E_mail = email,
                E_mail2 = email2,
                E_mail3 = email3,
                NickName = nickname,
                Company = company,
                Title = title,
                Fax = fax,
                SecondaryAddress = secondaryAddress,
                SecondaryHomePhone = secondaryHomePhone,
                Homepage = homepage,
                Notes = notes
            };
        }

        //Получаем элементы с формы свойств контакта, парсим по нужным полям.
        public EntryDate GetContactInformationFromDetails(int index)
        {
            manager.Navigator.GoToHome();
            manager.Contact.SelectContactDetails(index);
            string allText = driver.FindElement(By.Id("content")).Text;
            string fml = driver.FindElement(By.Id("content")).FindElement(By.TagName("b")).Text;

            return new EntryDate()
            {
                FML = fml,
                AllInfo = allText
            };
        }

        //1 lvl         
        ///////////////////////////////////////////////////////////////////////////
        public ContactHelper GoToAddNewEntry()
        {
            driver.FindElement(By.LinkText("add new")).Click();
            return this;
        }

        public ContactHelper FillEntryForm(EntryDate entry)
        {
            Type(By.Name("firstname"), entry.FirstName);
            Type(By.Name("middlename"), entry.MiddleName);
            Type(By.Name("lastname"), entry.LastName);
            Type(By.Name("address"), entry.Address);
            Type(By.Name("mobile"), entry.MobilePhone);
            Type(By.Name("email"), entry.E_mail);
            Type(By.Name("email2"), entry.E_mail2);
            Type(By.Name("email3"), entry.E_mail3);
            Type(By.Name("nickname"), entry.NickName);
            Type(By.Name("company"), entry.Company);
            Type(By.Name("title"), entry.Title);
            Type(By.Name("home"), entry.HomePhone);
            Type(By.Name("work"), entry.WorkPhone);
            Type(By.Name("fax"), entry.Fax);
            Type(By.Name("homepage"), entry.Homepage);
            Type(By.Name("phone2"), entry.SecondaryHomePhone);
            Type(By.Name("address2"), entry.SecondaryAddress);
            Type(By.Name("notes"), entry.Notes);


            //Чекнем, есть ли данный выпадающий список на странице. Так как форма создания и форма редактирования разные.
            if (IsElementPresent(By.Name("new_group")))
            {
                //Выбор значения из выпадающего списка не равного "none"
                List<IWebElement> options = driver.FindElement(By.Name("new_group")).FindElements(By.TagName("option")).ToList();
                for (int i = 0; i < options.Count; i++)
                {
                    string element;
                    element = options[i].GetAttribute("value");
                    if (!element.Equals("[none]"))
                        options[i].Click();
                    else options[0].Click();
                }
            }
            return this;
        }

        public ContactHelper SubmitNewEntry()
        {
            driver.FindElement(By.Name("submit")).Click();
            contactCash = null;
            return this;
        }

        public ContactHelper SubmitUpdateEntry()
        {
            driver.FindElement(By.CssSelector("[value='Update']")).Click();
            contactCash = null;
            return this;
        }

        public ContactHelper SubmitDeleteEntry()
        {
            driver.FindElement(By.CssSelector("[value='Delete']")).Click();
            contactCash = null;
            return this;
        }

        public ContactHelper SelectContact(int index)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + index + "]")).Click();
            return this;
        }

        public ContactHelper SelectContact(string id)
        {
            //Пример запроса: //input[@name='selected[]' and @value='229']
            driver.FindElement(By.XPath("(//input[@name='selected[]' and @value=" + id + "])")).Click();
            return this;
        }

        public ContactHelper DeleteContact()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            contactCash = null;
            return this;
        }

        //Подтверждение закрытия окна предупреждения
        public ContactHelper ClosedAlert()
        {
            driver.SwitchTo().Alert().Accept();
            return this;
        }

        ////Для отображения контактов в конкретной группе(тестовый способ, можно настроить)
        //public ContactHelper SelectGroupView()
        //{
        //    List<IWebElement> options = driver.FindElement(By.Name("group")).FindElements(By.TagName("option")).ToList();
        //    for (int i = 0; i < options.Count; i++)
        //    {
        //        string element;
        //        element = options[i].GetAttribute("value");
        //        if (!element.Equals("[none]") & !element.Equals("test2name"))
        //            options[i].Click();
        //    }
        //    return this; 
        //}

        public void ClearGroupFilter()
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText("[all]");
        }

        public ContactHelper SelectGroupToAdd(string name)
        {
            if (IsElementPresent(By.Name("to_group")))
            {
                new SelectElement(driver.FindElement(By.Name("to_group"))).SelectByText(name);
            }
            return this;
        }

        public ContactHelper AddToGroupClick()
        {
            driver.FindElement(By.Name("add")).Click();
            return this;
        }

        public ContactHelper RemoveToGroupClick()
        {
            driver.FindElement(By.CssSelector("[name=remove]")).Click();
            return this;
        }

        public ContactHelper SelectContactChange(int index)
        {
            if (IsElementPresent(By.CssSelector("a[href^='edit.php?']")))
            {
                //Создаем список елементов состоящий из ссылок имеющих название "edit.php?" и выбираем нужный из существующих.
                IList<IWebElement> elements = driver.FindElements(By.CssSelector("a[href^='edit.php?']"));
                if (index < elements.Count && index > 0)
                    elements[index - 1].Click();
            }
            return this;
        }

        //Ищем строку из таблицы у которой id первого поля равно "string id", кликаем по ссылки из этой строки.
        public ContactHelper SelectContactChange(string id)
        {
            if (IsElementPresent(By.CssSelector("tr[name=entry]")))
            {

                driver.FindElement(By.CssSelector("a[href='edit.php?id=" + id)).Click();

                //Неебический костыль
                //IList<IWebElement> row = driver.FindElements(By.CssSelector("tr[name=entry]"));
                //foreach(IWebElement r in row)
                //{
                //    IList<IWebElement> cell = r.FindElements(By.TagName("td"));
                //    if (cell[0].FindElement(By.TagName("input")).GetAttribute("id") == id)
                //        
                //}
            }
            return this;
        }

        public ContactHelper SelectContactDetails(int index)
        {
            if (IsElementPresent(By.CssSelector("a[href^='view.php?id']")))
            {
                //Создаем список елементов состоящий из ссылок имеющих название "edit.php?" и выбираем нужный из существующих.
                IList<IWebElement> elements = driver.FindElements(By.CssSelector("a[href^='view.php?id']"));
                if (index < elements.Count && index > 0)
                    elements[index - 1].Click();
            }
            return this;
        }

        public void SelectGroupView(string name)
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText(name);
        }
    }
}
