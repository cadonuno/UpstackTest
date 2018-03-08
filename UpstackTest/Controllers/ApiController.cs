using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UpstackTest.MockDb;
using System.Web;
using System.Net;

namespace UpstackTest.Controllers
{
    [Route("api/contacts")]
    public class ApiController : Controller
    {
        // GET api/contacts
        [HttpGet]
        public IEnumerable<Contact> Get()
        {
            return MockDatabaseConnection.Contacts;
        }

        // GET api/contacts/{arguments}
        [HttpGet("{arguments}", Name = "Get")]
        public IEnumerable<Contact> Get(string arguments)
        {
            arguments = arguments.ToLower();
            Boolean isNumber = int.TryParse(arguments, out int iArgument);
            if (!isNumber)
            {
                iArgument = 0;
            }

            return MockDatabaseConnection.Contacts.Where
            (
                x=> x.Address.ToLower().Contains(arguments) || 
                    x.Name.ToLower().Contains(arguments) ||
                    x.PhoneNumber.ToLower().Contains(arguments) ||
                    x.Id.Equals(iArgument)
            );
        }

        // POST api/contacts
        [HttpPost]
        public void Post([FromBody]Contact contact)
        {
            if (contact == null)
            {
                ThrowNullDataException();
            }

            Contact toAdd;
            if (contact.Id == null || contact.Id <= 0)
            {
                toAdd = new Contact(contact);
                MockDatabaseConnection.Contacts.Add(toAdd);
            }
            else
            {
                toAdd = MockDatabaseConnection.Contacts.Where(a => a.Id.Equals(contact.Id)).FirstOrDefault();
                if (toAdd == null)
                {
                    toAdd = new Contact(contact);
                    MockDatabaseConnection.Contacts.Add(toAdd);
                }
                else
                {
                    toAdd.Address = contact.Address;
                    toAdd.Name = contact.Name;
                    toAdd.PhoneNumber = contact.PhoneNumber;
                }
            }
        }
        // DELETE api/contacts/{id}
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            if (id <= 0)
            {
                ThrowNullDataException();                
            }
            else
            {
                Contact toDelete = MockDatabaseConnection.Contacts.Where(a => a.Id.Equals(id)).FirstOrDefault();
                if (toDelete == null)
                {
                    ThrowInexistentElementException();
                }
                MockDatabaseConnection.Contacts.Remove(toDelete);
            }
        }

        private void ThrowNullDataException()
        {
            throw new Exception("Invalid data - data must not be empty");
        }

        private void ThrowInexistentElementException()
        {
            throw new Exception("Nonexistent element - can't delet unexistent element");
        }
    }
}
