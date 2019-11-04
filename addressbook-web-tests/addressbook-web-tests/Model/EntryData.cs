using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddessbookTests
{
    public class EntryDate : IEquatable<EntryDate>, IComparable<EntryDate>
    {
        private string first;
        private string middle;
        private string last;
        private string address;
        private string telephone;
        private string e_mail;

        public string FirstName { get { return first; } set { first = value; } }
        public string MiddleName { get { return middle; } set { middle = value; } }
        public string LastName { get { return last; } set { last = value; } }
        public string Address { get { return address; } set { address = value; } }
        public string Telephone { get { return telephone; } set { telephone = value; } }
        public string E_mail { get { return e_mail; } set { e_mail = value; } }

        public EntryDate()
        {
            first = "";
            middle = "";
            last = "";
            address = "";
            telephone = "";
            e_mail = "";
        }

        public EntryDate(string first, string last)
        {
            this.first = first;
            this.last = last;
            this.address = "";
            middle = "";
            telephone = "";
            e_mail = "";
        }

        public EntryDate (string first, string last, string address)
        {
            this.first = first;
            this.last = last;
            this.address = address;
            middle = "";
            telephone = "";
            e_mail = "";
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
            return FirstName.GetHashCode() * LastName.GetHashCode() ; 
        }

        public override string ToString()
        {
            return "name=" + FirstName;
        }

        //Для сортировки списков, сравнение
        //Вернет 1 если текущий объект больше, вернет 0 если равны, вернет -1 если текущий меньше
        //
        public int CompareTo(EntryDate other)
        {
            if (Object.ReferenceEquals(other, null))
                return 1;
            return FirstName.CompareTo(other.FirstName);
        }
    }

}

