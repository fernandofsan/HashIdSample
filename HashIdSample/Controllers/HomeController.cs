using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using HashIdSample.Filters;
using Microsoft.AspNetCore.Mvc;
using HashIdSample.Models;
using HashidsNet;
using HashIdSample.Helpers;

namespace HashIdSample.Controllers
{
    public class Contact
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
    }

    public class HomeController : Controller
    {
        public List<Contact> MyContacts {
            get
            {
                Randomizer.Seed = new Random(0);
                var contactId = 0;
                var items = new Faker<Contact>()
                    .CustomInstantiator(x => new Contact() { Id = ++contactId })
                    .RuleFor(x => x.FirstName, y => y.Name.FirstName())
                    .Generate(20);

                return items;
            }
        } 

        [JOIHashIdFilter]
        public IActionResult Index()
        {            
            return View(MyContacts);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [JOIHashIdFilter]
        public IActionResult Contact(long id)
        {
            return View(MyContacts.FirstOrDefault(x => x.Id.Equals(id)));
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
