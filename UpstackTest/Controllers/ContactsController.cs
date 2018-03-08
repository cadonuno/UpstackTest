using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UpstackTest.MockDb;

namespace UpstackTest.Controllers
{
    public class ContactsController : Controller
    {
        public ActionResult Index(string arguments)
        {
            List<Contact> contacts;
            if (arguments != null)
            {
                contacts = new ApiController().Get(arguments).ToList();
                ViewBag.Search = arguments;
            }
            else
            {
                contacts = new ApiController().Get().ToList();
            }
            return View("/Views/Contacts/Index.cshtml", contacts);
        }

        public ActionResult New()
        {
            Contact contact = new Contact();
            return View("/Views/Contacts/Add.cshtml", contact);
        }

        public ActionResult Save(Contact contact)
        {
            try
            {
                new ApiController().Post(contact);
            }
            catch (Exception e)
            {
                ViewBag.Color = "red";
                ViewBag.Message = e.Message;
                return Index(null);
            }
            ViewBag.Color = "green";
            ViewBag.Message = "Contacted saved successfully";
            return Index(null);
            
        }

        public ActionResult Alter(int id)
        {
            Contact contact= new ApiController().Get(id.ToString()).FirstOrDefault();
            if (contact == null)
            {
                ViewBag.Color = "red";
                ViewBag.Message = "Selected contact not found";
                return Index(null);
            }
            return View("/Views/Contacts/Add.cshtml", contact);
        }

        public ActionResult Delete(int id)
        {
            try
            {
                new ApiController().Delete(id);
            }
            catch (Exception e)
            {
                ViewBag.Color = "red";
                ViewBag.Message = e.Message;
                return Index(null);
            }
            ViewBag.Color = "green";
            ViewBag.Message = "Contacted deleted successfully";
            return Index(null);

        }
    }
}