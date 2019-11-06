using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;


namespace WebAddessbookTests
{
    public class ContactHelper : BaseHelper
    {
        private List<EntryDate> contactCash = null;

        public ContactHelper(ApplicationManager manager) : base(manager)
        {
        }

        //2 lvl 
        public ContactHelper Create(EntryDate entry)
        {
            manager.Navigator.OpenStartPage();
            GoToAddNewEntry();
            FillEntryForm(entry);
            SubmitNewEntry();
            manager.Navigator.GoToHome();
            return this;
        }

        public ContactHelper ChangeGroup(int v)
        {
            manager.Navigator.OpenStartPage();
            manager.Navigator.GoToHome();
            SelectContact(v);
            SelectGroupAddTo();
            AddToGroupClick();
            manager.Navigator.GoToHome();
            return this;
        }

        public ContactHelper Removal(int v)
        {
            manager.Navigator.OpenStartPage();
            manager.Navigator.GoToHome();
            SelectContact(v+1);
            DeleteContact();
            ClosedAlert();
            manager.Navigator.GoToHome();
            return this;
        }

        public ContactHelper Edit(int v, EntryDate entry)
        {
            manager.Navigator.OpenStartPage();
            manager.Navigator.GoToHome();
            SelectContactChange(v+1);
            FillEntryForm(entry);
            SubmitUpdateEntry();
            manager.Navigator.GoToHome();
            return this;
        }

        public ContactHelper Delete(int v)
        {
            manager.Navigator.OpenStartPage();
            manager.Navigator.GoToHome();
            SelectContactChange(v+1);
            SubmitDeleteEntry();
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

        //1 lvl 
        public ContactHelper GoToAddNewEntry() 
        { 
            driver.FindElement(By.LinkText("add new")).Click(); 
            return this; 
        }

        public ContactHelper FillEntryForm(EntryDate entry)
        {
            Type(By.Name("firstname"), entry.FirstName);
            Type(By.Name("lastname"), entry.LastName);
            Type(By.Name("address"), entry.Address);
            Type(By.Name("middlename"), entry.MiddleName);
            Type(By.Name("mobile"), entry.Telephone);
            Type(By.Name("email"), entry.E_mail);

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

        public ContactHelper SelectContactChange(int v)
        {
            if (IsElementPresent(By.CssSelector("a[href^='edit.php?']")))
            {
                //Создаем список елементов состоящий из ссылок имеющих название "edit.php?" и выбираем нужный из существующих.
                List<IWebElement> elements = driver.FindElements(By.CssSelector("a[href^='edit.php?']")).ToList();
                if (v <= elements.Count && v > 0)
                    elements[v-1].Click();                                
            }
            return this;
        }
    }
}
