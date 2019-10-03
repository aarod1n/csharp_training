using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddessbookTests
{
    public class GroupData
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
    }
}
