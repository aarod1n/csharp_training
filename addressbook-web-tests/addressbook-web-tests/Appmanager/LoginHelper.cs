using OpenQA.Selenium;


namespace WebAddessbookTests
{
    public class LoginHelper : BaseHelper
    {
        public LoginHelper(IWebDriver driver) : base(driver)  
        {            
        }

        public void Login(AccountData user)
        {
            driver.FindElement(By.Name("user")).Click();
            driver.FindElement(By.Name("user")).Clear();
            driver.FindElement(By.Name("user")).SendKeys(user.UserName);
            driver.FindElement(By.Name("pass")).Clear();
            driver.FindElement(By.Name("pass")).SendKeys(user.Password);
            driver.FindElement(By.CssSelector("input[type=\"submit\"]")).Click();
        }

        //Метод для logoff'a
        public void logoff()
        {
            driver.FindElement(By.LinkText("Logout")).Click();
        }
    }
}
