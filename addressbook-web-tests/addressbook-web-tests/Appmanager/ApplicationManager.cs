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

        //Свойство для передачи значения драйвера и BaseURL
        public IWebDriver Driver
        {
            get { return driver; }
        }

        public string BaseURL
        {
            get { return baseURL; }
        }

        public ApplicationManager()
        {
            driver = new ChromeDriver();
            baseURL = "http://localhost/addressbook";

            //Передаем ссылку на самого себя(ApplicationManager) помощникам, что бы пользоваться экземпляром объекта ApplicationManager и забирать нужные поля через свойства.
            loginHelper = new LoginHelper(this);
            navigationHelper = new NavigationHelper(this);
            groupHelper = new GroupHelper(this);
            contactHelper = new ContactHelper(this);
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
