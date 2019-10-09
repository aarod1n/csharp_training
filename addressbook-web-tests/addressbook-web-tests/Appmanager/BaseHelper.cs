using OpenQA.Selenium;

/// <summary>
/// Данным классом убираем дублирование объявления переменной IWebDriver driver
/// </summary>

namespace WebAddessbookTests
{
    public class BaseHelper
    {
        protected IWebDriver driver;

        public BaseHelper(IWebDriver driver)
        {
            this.driver = driver;
        }
    }
}
