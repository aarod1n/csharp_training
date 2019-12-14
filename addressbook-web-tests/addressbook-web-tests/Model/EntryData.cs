using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using LinqToDB.Mapping;

namespace WebAddessbookTests
{
    [Table(Name = "addressbook")]
    public class EntryDate : IEquatable<EntryDate>, IComparable<EntryDate>
    {
        private string allPhone;
        private string allEmail;
        private string fml;
        private string allInfo;
        
        //Свойства

        //ID помечен как 
        [Column(Name = "id"), PrimaryKey]
        public string Id { get; set; }
        
        [Column(Name = "firstname")]
        public string FirstName { get; set; }

        [Column(Name = "middlename")]
        public string MiddleName { get; set; }

        [Column(Name = "lastname")]
        public string LastName { get; set; }

        [Column(Name = "nickname")]
        public string NickName { get; set; }

        [Column(Name = "title")]
        public string Title { get; set; }

        [Column(Name = "company")]
        public string Company { get; set; }

        [Column(Name = "address")]
        public string Address { get; set; }

        [Column(Name = "mobile")]
        public string MobilePhone { get; set; }

        [Column(Name = "home")]
        public string HomePhone { get; set; }

        [Column(Name = "work")]
        public string WorkPhone { get; set; }

        [Column(Name = "fax")]
        public string Fax { get; set; }

        [Column(Name = "phone2")]
        public string SecondaryHomePhone { get; set; }

        [Column(Name = "email")]
        public string E_mail { get; set; }

        [Column(Name = "email2")]
        public string E_mail2 { get; set; }

        [Column(Name = "email3")]
        public string E_mail3 { get; set; }

        [Column(Name = "homepage")]
        public string Homepage { get; set; }

        [Column(Name = "address2")]
        public string SecondaryAddress { get; set; }

        [Column(Name = "notes")]
        public string Notes { get; set; }

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
                    return (CheckOnNull("", FirstName) + CheckOnNull(" ", MiddleName) + CheckOnNull(" ", LastName)).Trim();
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
                    return 
                        (
                        CheckOnNull("", FirstName) + CheckOnNull(" ", MiddleName) + CheckOnNull(" ", LastName)
                        + CheckOnNull("\r\n", NickName)
                        + CheckOnNull("\r\n", Title)
                        + CheckOnNull("\r\n", Company)
                        + CheckOnNull("\r\n", Address)
                        + CheckOnNull("\r\n\r\nH: ", HomePhone)
                        + CheckOnNull("\r\nM: ", MobilePhone)
                        + CheckOnNull("\r\nW: ", WorkPhone)
                        + CheckOnNull("\r\nF: ", Fax)
                        + CheckOnNull("\r\n\r\n", E_mail)
                        + CheckOnNull("\r\n", E_mail2)
                        + CheckOnNull("\r\n", E_mail3)
                        + CheckOnNull("\r\nHomepage:\r\n", Homepage)
                        + CheckOnNull("\r\n\r\n\r\n", SecondaryAddress)
                        + CheckOnNull("\r\n\r\nP: ", SecondaryHomePhone)
                        + CheckOnNull("\r\n\r\n", Notes)
                        ).Trim();
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
            
        }

        public EntryDate(string first, string last, string address)
        {
            FirstName = first;
            LastName = last;
            Address = address;
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
            return "\nID= " + Id + "\nname= " + FirstName + "\nmiddleName= " + MiddleName + "\nlastName= " + LastName;
        }

        //Для сортировки списков, сравнение
        //Вернет 1 если текущий объект больше, вернет 0 если равны, вернет -1 если текущий меньше
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


        //public int CompareTo(EntryDate other)
        //{
        //    if (Object.ReferenceEquals(other, null))
        //        return 1;
        //    return Id.CompareTo(other.Id);
        //}

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

        public string CheckOnNull(string addText, string text)
        {
            if (text != "") return addText + text;
            else return "";
        }

        public static List<EntryDate> GetAll()
        {   //Создаем подключение к БД db
            //Возвращаем список
            //Конструкция using используется для закрытия соединения с БД, db.Close() писать не нужно, выполнится автоматом.
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from g in db.Contacts select g).ToList();
            }
        }
    }

}

