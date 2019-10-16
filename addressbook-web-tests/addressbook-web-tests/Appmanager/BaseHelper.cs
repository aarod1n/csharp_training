using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;

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

        //Проверка на наличие элемента на странице
        //Нет - вернет null, есть - вернет первый найденый
        public IWebElement GetExistElement(By locator)
        {
            List<IWebElement> elements = driver.FindElements(locator).ToList();
            if (elements.Count > 0)
                return elements[0];
            return null;
        }
    }
}
