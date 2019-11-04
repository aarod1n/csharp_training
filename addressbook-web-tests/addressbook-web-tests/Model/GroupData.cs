using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddessbookTests
{
    public class GroupData : IEquatable<GroupData>, IComparable<GroupData>
    {
        //Поля класса
        private string name;
        private string header;
        private string footer;

        //Свойства класса
        public string GroupName
        {
            get { return name; }
            set { name = value; }
        }
        public string GroupHeader
        {
            get { return header; }
            set { header = value; }
        }
        public string GroupFooter
        {
            get { return footer; }
            set { footer = value; }
        }

        //Конструкторы класса
        public GroupData() { }

        public GroupData(string name)
        {
            this.name = name;
            this.header = "";
            this.footer = "";
        }
                
        public GroupData(string name, string header, string footer)
        {
            this.name = name;
            this.header = header;
            this.footer = footer;
        }

        //Методы класса

        //Учим сравнивать объекты типа GroupData
        public bool Equals(GroupData other) //метод из public interface IEquatable<T>
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }
            return GroupName == other.GroupName;
        }

        //Для оптимизации сравнения, сначала сравниваем хешкоды полученного поля из геттера свойства GroupName.
        //Если они равны, значит объекты одинаковые, если нет продолжаем равнивать bool Equals
        public override int GetHashCode()
        {
            return GroupName.GetHashCode();
        }

        public override string ToString()
        {
            return "name=" + GroupName;
        }

        //Для сортировки списков, сравнение
        //Вернет 1 если текущий объект больше, вернет 0 если равны, вернет -1 если текущий меньше
        //
        public int CompareTo(GroupData other)
        {
            if (Object.ReferenceEquals(other, null))
                return 1;
            return GroupName.CompareTo(other.GroupName); //Сравнение по смыслу имен групп
        }
    }
}
