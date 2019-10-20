using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace WebAddessbookTests
{
    public class GroupHelper : BaseHelper
    {
        public GroupHelper(ApplicationManager manager) : base(manager) 
        {
        }
        
        //2 lvl
        public GroupHelper Created(GroupData group)
        {
            manager.Navigator.OpenStartPage();
            GoToGroupPage();
            InitNewGroupCreation();
            FillGroupForm(group);
            SubmitCreatedGroup();
            GoToGroupPage();
            return this;
        }

        public GroupHelper Modify(int v, GroupData newData)
        {
            manager.Navigator.OpenStartPage();
            GoToGroupPage();
            SelectGroup(v);
            EditGroup();
            FillGroupForm(newData);
            UpdateGroup();
            GoToGroupPage();
            return this;
        }

        public GroupHelper RemovaGroup(int g)
        {
            manager.Navigator.OpenStartPage();
            GoToGroupPage();
            SelectGroup(g);
            DeleteGroup();
            GoToGroupPage();
            return this;
        }

        //Проверка
        public GroupHelper CheckPresenceGroup(GroupData group)
        {
            manager.Navigator.OpenStartPage();
            GoToGroupPage();
            if (!(IsElementPresent(By.LinkText("groups"))))
            {
                manager.Group.Created(group);
            }
            return this;
        }

        //1 lvl
        public GroupHelper GoToGroupPage()
        {
            driver.FindElement(By.LinkText("groups")).Click();
            return this;
        }
        public GroupHelper SubmitCreatedGroup()
        {
            driver.FindElement(By.Name("submit")).Click();
            return this;
        }
        public GroupHelper InitNewGroupCreation()
        {
            driver.FindElement(By.Name("new")).Click();
            return this;
        }
        public GroupHelper FillGroupForm(GroupData group)
        {
            //Используем метод, убираем дублирование кода
            Type(By.Name("group_name"), group.GroupName);
            Type(By.Name("group_header"), group.GroupHeader);
            Type(By.Name("group_footer"), group.GroupFooter);
            return this;
        }

        //Выбор из доступных созданных групп
        public GroupHelper SelectGroup(int index)
        {
            if (IsElementPresent(By.CssSelector("input[name='selected[]']"))) //[class='group']
            {
                List<IWebElement> elements = driver.FindElements(By.CssSelector("input[name='selected[]']")).ToList();
                if(index <= elements.Count && index > 0)
                {
                    driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + index + "]")).Click();
                }

            }
            return this;
        }

        public GroupHelper DeleteGroup()
        {
            driver.FindElement(By.Name("delete")).Click();
            return this;
        }

        public GroupHelper EditGroup()
        {
            driver.FindElement(By.Name("edit")).Click();
            return this;
        }

        public GroupHelper UpdateGroup()
        {
            driver.FindElement(By.Name("update")).Click(); 
            return this;
        }
    }
}
