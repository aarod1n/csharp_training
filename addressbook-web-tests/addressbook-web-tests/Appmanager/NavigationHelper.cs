
using OpenQA.Selenium;


namespace WebAddessbookTests
{
    public class NavigationHelper : BaseHelper
    {
        private string baseURL;
        public NavigationHelper(IWebDriver driver, string baseURL) : base(driver) { this.baseURL = baseURL; }
        
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
