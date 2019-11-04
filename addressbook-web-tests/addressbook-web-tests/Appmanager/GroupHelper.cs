using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;

namespace WebAddessbookTests
{
    public class GroupHelper : BaseHelper
    {
        //Приватный кеш
        private List<GroupData> groupCash = null;

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
            SelectGroup(v+1);
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
            SelectGroup(g+1);
            DeleteGroup();
            GoToGroupPage();
            return this;
        }

        //Проверка наличие групп
        public GroupHelper CheckPresenceGroup(GroupData group)
        {
            manager.Navigator.OpenStartPage();
            GoToGroupPage();
            if (!(IsElementPresent(By.CssSelector("input[name='selected[]']"))))
            {
                manager.Group.Created(group);
            }
            return this;
        }

        //Получение всех созданных групп, метод реализован с помощью кеширования
        public List<GroupData> GetGroupList()
        {
            if (groupCash == null)
            {
                groupCash = new List<GroupData>();
                manager.Navigator.OpenStartPage();
                GoToGroupPage();
                ICollection<IWebElement> elements = driver.FindElements(By.CssSelector("span.group"));
                foreach (IWebElement e in elements)
                {
                    groupCash.Add(new GroupData(e.Text));//Создаем объекты типа GroupData, кладем в список 
                }                
            }
            return new List<GroupData>(groupCash);
        }

        //Сравниваем кол-во элементов в списках
        public void CheckGroupCreationResultByCount(List<GroupData> oldGroupList, List<GroupData> newGroupList)
        {
            Assert.AreEqual(oldGroupList.Count + 1, newGroupList.Count); //Для нашей реализации нужна логика Assert из dll NUnit.Framework
        }

        public void CheckGroupRemovalResultByCount(List<GroupData> oldGroupList, List<GroupData> newGroupList)
        {
            Assert.AreEqual(oldGroupList.Count - 1, newGroupList.Count); //Для нашей реализации нужна логика Assert из dll NUnit.Framework
        }

        //Сравниваем два объекта типа GroupData
        public void CheckGroupResultByObj(List<GroupData> oldGroupList, List<GroupData> newGroupList)
        {
            oldGroupList.Sort();
            newGroupList.Sort();
            Assert.AreEqual(oldGroupList, newGroupList);
        }

        public void CheckChangeGroupResultByObj(int index, GroupData group, List<GroupData> oldGroupList, List<GroupData> newGroupList)
        {
            oldGroupList.Sort();
            oldGroupList[index].GroupName = group.GroupName;
            newGroupList.Sort();
            Assert.AreEqual(oldGroupList, newGroupList);
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
            groupCash = null;
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
            groupCash = null;
            return this;
        }

        public GroupHelper EditGroup()
        {
            driver.FindElement(By.Name("edit")).Click();
            groupCash = null;
            return this;
        }

        public GroupHelper UpdateGroup()
        {
            driver.FindElement(By.Name("update")).Click();
            groupCash = null;
            return this;
        }
    }
}
