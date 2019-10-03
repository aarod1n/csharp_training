using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAddessbookTests
{
    class EntryDate
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

        public EntryDate (string first, string last, string address)
        {
            this.first = first;
            this.last = last;
            this.address = address;
            middle = "";
            telephone = "";
            e_mail = "";
        }
    }

}

