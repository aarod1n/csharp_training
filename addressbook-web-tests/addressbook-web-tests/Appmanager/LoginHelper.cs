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

                Logoff();
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
            return IsLoggetIn() 
                && GetLoggetUserName() == user.UserName;
        }

        public string GetLoggetUserName()
        {
            string text = driver.FindElement(By.Name("logout")).FindElement(By.TagName("b")).Text;
            return text.Substring(1, text.Length - 2);
        }

        //Метод для logoff'a
        public void Logoff()
        {
            if (IsLoggetIn())
            {
                driver.FindElement(By.LinkText("Logout")).Click();
            }
        }
    }
}
