using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Text;

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
        public void SetupApplicationManager()
        {
            AppManager = ApplicationManager.GetInstance();            
        }

        //Генератор
        public static Random rnd = new Random();

        public static string GenerationRandomString(int max, bool onlyNumbers)
        {
            //NextDouble() генерит число от 0 до 1
            //Создаем число от 0 до max.
            int l = Convert.ToInt32(rnd.NextDouble() * max);
            //Билдер для построения строк
            StringBuilder builder = new StringBuilder();

            if (!onlyNumbers)
            {
                for (int i = 0; i < l; i++)
                {
                    //Случайное число конвертим в символ, символы с кодом 0-32 не печатные, сдвигаем код символа, добавляем в билдер.
                    builder.Append(Convert.ToChar(32 + Convert.ToInt32(rnd.NextDouble() * 223)));
                }
                return builder.ToString();
            }
            else
            {
                for (int i = 0; i < l; i++)
                {
                    builder.Append(Convert.ToChar(48 + Convert.ToInt32(rnd.NextDouble() * 57)));
                }
                return builder.ToString();
            }
        }
    }
}
