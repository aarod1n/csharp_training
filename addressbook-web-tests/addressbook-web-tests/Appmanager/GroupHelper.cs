using OpenQA.Selenium;
using System;
using System.Collections.Generic;

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
            GoToGroupPage();
            InitNewGroupCreation();
            FillGroupForm(group);
            SubmitCreatedGroup();
            GoToGroupPage();
            return this;
        }

        public GroupHelper Modify(int v, GroupData newData)
        {
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
            GoToGroupPage();
            SelectGroup(g);
            DeleteGroup();
            GoToGroupPage();
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
            driver.FindElement(By.Name("group_name")).Clear();
            driver.FindElement(By.Name("group_name")).SendKeys(group.GroupName);
            driver.FindElement(By.Name("group_header")).Clear();
            driver.FindElement(By.Name("group_header")).SendKeys(group.GroupHeader);
            driver.FindElement(By.Name("group_footer")).Clear();
            driver.FindElement(By.Name("group_footer")).SendKeys(group.GroupFooter);
            return this;
        }

        public GroupHelper SelectGroup(int index)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + index + "]")).Click();
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
