using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB;

namespace WebAddessbookTests
{
    //Наследуем данный класс от LinqToDB.Data.DataConnection для представления им БД
    public class AddressBookDB : LinqToDB.Data.DataConnection
    {
        //Передаем в базовый класс connectionStrings из app.config для подключения к БД
        public AddressBookDB() : base("AddressBook") { }

        //Свойства
        //Возвращаем таблицу данных GroupData для построения списка соответствующего типа
        public ITable<GroupData> Groups { get { return GetTable<GroupData>(); } }

        //Возвращаем таблицу данных EntryDate для построения списка соответствующего типа
        public ITable<EntryDate> Contacts { get { return GetTable<EntryDate>(); } }

        public ITable<GroupContactRelation> GCR { get { return GetTable<GroupContactRelation>(); } }
    }
}
