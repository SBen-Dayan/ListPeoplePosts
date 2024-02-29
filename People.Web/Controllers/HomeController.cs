using Microsoft.AspNetCore.Mvc;
using People.Data;
using People.Web.Models;
using System.Diagnostics;

namespace People.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly PeopleManager _manager = new(@"Data Source=.\sqlexpress; Initial Catalog=Practice; Integrated Security=True;");

        public IActionResult Index()
        {
            return View(new PeopleViewModel
            {
                People = _manager.GetPeople(),
                Message = TempData["message"] != null ? (string)TempData["message"] : null
            });
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(List<Person> people) 
        {
            people.RemoveAll(p => string.IsNullOrEmpty(p.FirstName) || string.IsNullOrEmpty(p.LastName) || p.Age == default);
            if(people.Count == 0)
            {
                return Redirect("/home/add");
            }
            _manager.AddPeople(people);
            TempData["message"] = $"{(people.Count > 1? "people": "person")} added successfully!";
            return Redirect("/");
        }

    }
}