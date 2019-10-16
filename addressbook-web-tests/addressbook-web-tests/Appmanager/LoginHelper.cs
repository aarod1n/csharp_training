using OpenQA.Selenium;


namespace WebAddessbookTests
{
    public class LoginHelper : BaseHelper
    {
        public LoginHelper(ApplicationManager manager) : base(manager)  
        {            
        }

        public void Login(AccountData user)
        {
            Type(By.Name("user"), user.UserName);
            Type(By.Name("pass"), user.Password);
            driver.FindElement(By.CssSelector("input[type=\"submit\"]")).Click();
        }

        //Метод для logoff'a
        public void logoff()
        {
            driver.FindElement(By.LinkText("Logout")).Click();
        }
    }
}
