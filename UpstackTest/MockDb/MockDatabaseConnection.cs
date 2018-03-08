using System.Collections.Generic;

namespace UpstackTest.MockDb
{
    public static class MockDatabaseConnection
    {
        //contacts table, allows me to use the Add method to simulate a LinqToEntities situation
        public static List<Contact> Contacts = new List<Contact>();
    }
}
