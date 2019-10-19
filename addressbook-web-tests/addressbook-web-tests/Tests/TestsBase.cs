using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

/// <summary>
/// Разгружаем базовый класс с настройками.
/// Все методы перенесли в классы хелперы. Тут создаем объекты классов хелперов. Учим тесты работать с методами данных объектов.
/// 
/// Создан менеджер для хелперов. Все ссылки на объекты хелперов перенесены.
/// Тут объявляем и инициализируем только ссылку на менеджера.
/// Учим тесты работать под управлением менеджера.
/// </summary>

namespace WebAddessbookTests
{
    public class TestsBase
    {
        protected ApplicationManager AppManager;

        [SetUp]
        public void SetupTest()
        {
            AppManager = ApplicationManager.GetInstance();
        }

    }
}
