using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace WebAddessbookTests
{
    public class EntryDate : IEquatable<EntryDate>, IComparable<EntryDate>
    {
        private string allPhone;
        private string allEmail;
        private string fml;
        private string allInfo;

        //Свойства
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public string Address { get; set; }
        public string MobilePhone { get; set; }
        public string HomePhone { get; set; }
        public string WorkPhone { get; set; }
        public string SecondaryHomePhone { get; set; }
        public string E_mail { get; set; }
        public string E_mail2 { get; set; }
        public string E_mail3 { get; set; }
        
        public string AllPhone
        {
            get
            {
                if (allPhone != null)
                {
                    return allPhone;
                }
                else
                {
                    // + Trim - уберает лишние пробелы с начала, конца строки.
                    return (CleanUp(HomePhone, 1) + CleanUp(MobilePhone, 1) + CleanUp(WorkPhone, 1) + CleanUp(SecondaryHomePhone, 1)).Trim();
                }
            }
            set
            {
                allPhone = value;
            }
        }

        public string AllEmail
        {
            get
            {
                if (allEmail != null)
                {
                    return allEmail;
                }
                else
                {
                    // + Trim - уберает лишние пробелы с начала, конца строки.
                    return (CleanUp(E_mail, 2) + CleanUp(E_mail2, 2) + CleanUp(E_mail3, 2)).Trim();
                }
            }
            set
            {
                allEmail = value;
            }
        }

        public string FML
        {
            get
            {
                if (fml != null)
                {
                    return fml;
                }
                else
                {
                    return (FirstName + " " + MiddleName + " " + LastName).Trim();
                }
            }
            set
            {
                fml = value;
            }
        }

        public string AllInfo 
        {
            get
            {
                if(allInfo != null)
                {
                    return allInfo;
                }
                else
                {
                    return (FirstName + " " + MiddleName + " " + LastName
                        + "\r\n" 
                        + Address
                        + "\r\n\r\n"
                        + "M: " + MobilePhone
                        + "\r\n\r\n"
                        + E_mail).Trim();
                }
            }
            set
            {
                allInfo = value;
            }
        }

        //Конструкторы
        public EntryDate()
        {
            FirstName = "";
            LastName = "";
            MiddleName = "";
            Address = "";
            MobilePhone = "";
            HomePhone = "";
            WorkPhone = "";
            E_mail = "";
        }

        public EntryDate(string first, string last)
        {
            FirstName = first;
            LastName = last;
            MiddleName = "";
            Address = "";
            MobilePhone = "";
            HomePhone = "";
            WorkPhone = "";
            E_mail = "";
        }

        public EntryDate(string first, string last, string address)
        {
            FirstName = first;
            LastName = last;
            Address = address;
            MiddleName = "";
            MobilePhone = "";
            HomePhone = "";
            WorkPhone = "";
            E_mail = "";
        }

        //Учим сравнивать объекты типа EntryDate
        public bool Equals(EntryDate other) //метод из public interface IEquatable<T>
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }

            return FirstName == other.FirstName && LastName == other.LastName;
        }

        //Для оптимизации сравнения, сначала сравниваем хешкоды полученного поля из геттера свойства GroupName.
        //Если они равны, значит объекты одинаковые, если нет продолжаем равнивать bool Equals
        public override int GetHashCode()
        {
            return FirstName.GetHashCode() * LastName.GetHashCode();
        }

        public override string ToString()
        {
            return "\nname= " + FirstName + "\nmiddleName= " + MiddleName + "\nlastName= " + LastName + "\ne-mail= " + E_mail;
        }

        //Для сортировки списков, сравнение
        //Вернет 1 если текущий объект больше, вернет 0 если равны, вернет -1 если текущий меньше
        //
        public int CompareTo(EntryDate other)
        {
            if (Object.ReferenceEquals(other, null))
                return 1;
            if (LastName.CompareTo(other.LastName) == 0)
            {
                return FirstName.CompareTo(other.FirstName);
            }
            else return LastName.CompareTo(other.LastName);
        }

        //Вариант option = 1 для склейки телефонов 
        //Берем строку, заменяем "-()" на "", добавляем в конец "\r\n", возвращаем.
        //Вариант option = 2 для склейки почты.
        //Берем строку и добавляем "\r\n", возвращаем.
        //Вариант 3 для склейки всех(пока выборочно) полей для 3 проверки
        private string CleanUp(string text, int option)
        {
            string result = "";

            switch (option)
            {
                case 1:
                    if (text == null || text == "")
                    {
                        result = "";
                    }
                    else
                    {
                        result = Regex.Replace(text, "[-()]", "") + "\r\n";
                    }
                    break;
                
                case 2:
                    if (text == null || text == "")
                    {

                        result = "";
                    }
                    else
                    {
                        result = text + "\r\n";
                    }
                    break;
            }

            return result;
        }

        
    }

}

