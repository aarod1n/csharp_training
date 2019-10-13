using OpenQA.Selenium;

/// <summary>
/// Данным классом убираем дублирование объявления переменной IWebDriver driver
/// </summary>

namespace WebAddessbookTests
{
    public class BaseHelper
    {
        protected ApplicationManager manager;
        protected IWebDriver driver;

        public BaseHelper(ApplicationManager manager)
        {
            this.manager = manager;
            driver = manager.Driver;
        }
    }
}
