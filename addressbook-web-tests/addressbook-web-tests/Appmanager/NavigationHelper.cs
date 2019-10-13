using OpenQA.Selenium;


namespace WebAddessbookTests
{
    public class NavigationHelper : BaseHelper
    {
        private string baseURL;
        public NavigationHelper(ApplicationManager manager) : base(manager) { baseURL = manager.BaseURL; }
        
        public void OpenStartPage()
        {
            driver.Navigate().GoToUrl(baseURL);
        }
        public void GoToHome()
        {
            driver.FindElement(By.LinkText("home")).Click();
        }
    }
}
