using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB.Mapping;


namespace WebAddessbookTests
{
    //Делаем привязку соответствия класса к таблице
    [Table(Name = "group_list")]
    public class GroupData : IEquatable<GroupData>, IComparable<GroupData>
    {
        //Свойства класса
        //Привязываем свойства к полям из таблици
        //Если мы собираемся не только читать данные из полей, но и писать в них, нужно обратить внимание, некоторые поля имеют свойство Not Null
        //В атрибуте можно указать NotNull
        [Column(Name = "group_name"), NotNull]
        public string GroupName { get; set; }
        
        [Column(Name = "group_header")]
        public string GroupHeader { get; set; }
        
        [Column(Name = "group_footer")]
        public string GroupFooter { get; set; }
        
        //Данное поле является уникальным ключем и идентификатором
        [Column(Name = "group_id"), PrimaryKey, Identity]
        public string Id { get; set; }


        //Конструкторы класса
        public GroupData() { }

        public GroupData(string name)
        {
            GroupName = name;
            GroupHeader = "";
            GroupFooter = "";
        }
                
        public GroupData(string name, string header, string footer)
        {
            GroupName = name;
            GroupHeader = header;
            GroupFooter = footer;
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
            return "\nID=" + Id +"\nName=" + GroupName + "\nHeader= " + GroupHeader + "\nFooter= " + GroupFooter;
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

        public static List<GroupData> GetAll()
        {   //Создаем подключение к БД db
            //Возвращаем список
            //Конструкция using используется для закрытия соединения с БД, db.Close() писать не нужно, выполнится автоматом.
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from g in db.Groups select g).ToList();
            }
        }

        public List<EntryDate> GetContact()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from c in db.Contacts
                        from gcr in db.GCR.Where(p => p.GroupID == this.Id  && p.ContactID == c.Id)
                        select c).Distinct().ToList();
            }
        }
    }
}
