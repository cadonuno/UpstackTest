using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UpstackTest.MockDb
{
    public class Contact
    {
        private static readonly object syncLock = new object();

        public static int Sequence = 0;

        public Contact(Contact contact)
        {
            this.Id = GetSequence();
            this.Address = contact.Address;
            this.Name = contact.Name;
            this.PhoneNumber = contact.PhoneNumber;
        }

        public Contact()
        {
            //empty constructor for creating an object without adding to the database
        }

        public static int GetSequence()
        {
            lock (syncLock)
            {
                return ++Sequence;
            }
        }

        public int? Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }
    }
}
