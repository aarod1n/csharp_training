using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddessbookTests
{
    public class AccountData
    {
        //Поля класса
        private string name;
        private string password;

        //Свойства класса
        public string UserName
        {
            get { return name; }
            set { name = value; }
        }
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        //Конструкторы класса
        public AccountData() { }

        public AccountData(string name, string password)
        {
            this.name = name;
            this.password = password;
        }
    }
}
