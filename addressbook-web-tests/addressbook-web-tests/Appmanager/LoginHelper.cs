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
            if (IsLoggetIn())
            {
                if (IsLoggetIn(user))
                {
                    return;
                }

                logoff();
            }
            Type(By.Name("user"), user.UserName);
            Type(By.Name("pass"), user.Password);
            driver.FindElement(By.CssSelector("input[type=\"submit\"]")).Click();
        }

        public bool IsLoggetIn()
        {
            return IsElementPresent(By.Name("logout"));
        }

        public bool IsLoggetIn(AccountData user)
        {
            return IsLoggetIn() && driver.FindElement(By.Name("logout")).FindElement(By.TagName("b")).Text == "(" + user.UserName + ")";
        }

        //Метод для logoff'a
        public void logoff()
        {
            if (IsLoggetIn())
            {
                driver.FindElement(By.LinkText("Logout")).Click();
            }
        }
    }
}
