using OpenQA.Selenium;

namespace WebAddessbookTests
{
    public class GroupHelper : BaseHelper
    {
        public GroupHelper(IWebDriver driver) : base(driver) 
        {
        }
        /// <summary>
        /// Методы для тестов групп
        /// </summary>
        public void GoToGroupPage()
        {
            driver.FindElement(By.LinkText("groups")).Click();
        }
        public void CreatedGroup()
        {
            driver.FindElement(By.Name("submit")).Click();
        }
        public void InitNewGroupCreation()
        {
            driver.FindElement(By.Name("new")).Click();
        }
        public void FillGroupForm(GroupData group)
        {
            driver.FindElement(By.Name("group_name")).Clear();
            driver.FindElement(By.Name("group_name")).SendKeys(group.GroupName);
            driver.FindElement(By.Name("group_header")).Clear();
            driver.FindElement(By.Name("group_header")).SendKeys(group.GroupHeader);
            driver.FindElement(By.Name("group_footer")).Clear();
            driver.FindElement(By.Name("group_footer")).SendKeys(group.GroupFooter);
        }
        public void SubmitGroupCreation()
        {
            driver.FindElement(By.LinkText("groups")).Click();
        }

        public void SelectGroup(int index)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + index + "]")).Click();
        }

        public void DeleteGroup()
        {
            driver.FindElement(By.Name("delete")).Click();
        }

    }
}
