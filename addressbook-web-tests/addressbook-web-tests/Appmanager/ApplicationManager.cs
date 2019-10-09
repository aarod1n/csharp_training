using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace WebAddessbookTests
{
    public class ApplicationManager
    {
        private string baseURL;
        private IWebDriver driver;

        private LoginHelper loginHelper;
        private NavigationHelper navigationHelper;
        private GroupHelper groupHelper;
        private ContactHelper contactHelper;


        public ApplicationManager()
        {
            driver = new ChromeDriver();
            baseURL = "http://localhost/addressbook";
            loginHelper = new LoginHelper(driver);
            navigationHelper = new NavigationHelper(driver, baseURL);
            groupHelper = new GroupHelper(driver);
            contactHelper = new ContactHelper(driver);
        }
    
        /// <summary>
        /// Свойства для получения значения приватных полей в тестах для вызова методов.
        /// </summary>
        public LoginHelper Auth { get { return loginHelper; } }
        public NavigationHelper Navigator { get { return navigationHelper; } }
        public GroupHelper Group { get { return groupHelper; } }
        public ContactHelper Contact { get { return contactHelper; } }
    
        public void Quit()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }
    }
}
