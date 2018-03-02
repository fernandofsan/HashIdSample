using Bogus;
using HashIdSample.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using HashIdSample.Models;

namespace HashIdSample.Controllers
{
    public class HashIdFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            foreach (var routeDataValue in context.RouteData.Values)
                if (routeDataValue.Key.EndsWith("id", StringComparison.OrdinalIgnoreCase))
                    context.ActionArguments[routeDataValue.Key] = HashIdsUtil.Decode(routeDataValue.Value);

            base.OnActionExecuting(context);
        }
    }

    public class HomeController : Controller
    {
        public List<Contact> AllContacts {
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

        public IActionResult Index()
        {
            var teste = HashIdsUtil.Encode(1);

            var te = HashIdsUtil.Decode(teste);

            return View(AllContacts);
        }

        [HashIdFilter]
        public IActionResult Contact(long id)
        {
            return View(AllContacts.FirstOrDefault(x => x.Id.Equals(id)));
        }
    }
}
