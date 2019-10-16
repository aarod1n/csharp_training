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

        //Проверка на наличие элемента на странице (увидел в лекциях 3.хD)
        //Вернет true если элемент найден, есди нет элемента, упадем в исключение, которое поймаем в блоке catch и обработаем как false.
        public bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        //Костыль пока оставлю, на память.
        //public IWebElement GetExistElement(By locator)
        //{
        //    List<IWebElement> elements = driver.FindElements(locator).ToList();
        //    if (elements.Count > 0)
        //        return elements[0];
        //    return null;
        //}

        //Метод для заполнения полей. Если не ввели значение для ввода, пропускаем заполнение.
        public void Type(By locator, string text)
        {
            if (text != null)
            {
                driver.FindElement(locator).Clear();
                driver.FindElement(locator).SendKeys(text);
            }
        }
    }
}
