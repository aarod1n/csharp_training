using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddessbookTests
{
    public class EntryDate : IEquatable<EntryDate>, IComparable<EntryDate>
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public string E_mail { get; set; }
        public string Id { get; set; }

        public EntryDate()
        {
            FirstName = "";
            MiddleName = "";
            LastName = "";
            Address = "";
            Telephone = "";
            E_mail = "";
        }

        public EntryDate(string first, string last)
        {
            FirstName = first;
            LastName = last;
            Address = "";
            MiddleName = "";
            Telephone = "";
            E_mail = "";
        }

        public EntryDate (string first, string last, string address)
        {
            FirstName = first;
            LastName = last;
            Address = address;
            MiddleName = "";
            Telephone = "";
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

