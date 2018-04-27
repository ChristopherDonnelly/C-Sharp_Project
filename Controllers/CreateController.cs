using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using C_Sharp_Project.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using VolunteerPlanner.Models;

namespace C_Sharp_Project.Controllers
{
    public class CreateController : Controller
    {
        private VolunteerContext _context;
        private string _controller;
        private string _action;

        public CreateController(VolunteerContext context)
        {
            _context = context;
            _controller = "User";
            _action = "Login";
        }

        [HttpGet]
        [Route("CreateLocation/{EventId}")]
        public IActionResult CreateLocation(int EventId)
        {
            if(isLoggedIn()){
                setSessionViewData();

                Event Event = _context.events.Include( j => j.EventVolunteers ).ThenInclude( u => u.User ).SingleOrDefault( e => e.EventId == EventId);
                List<Location> Locations = _context.locations.Include( j => j.Event ).Where(e => e.EventId == EventId).ToList();

                return View(new LocationsEventBundleModel{ Event = Event, Locations = Locations });
            }else{
                return RedirectToAction(_action, _controller);
            }
        }

        [HttpPost]
        [Route("AddLocation")]
        public JsonResult AddLocation([FromBody] Location location)
        {

            Console.WriteLine(location.EventId);
            Console.WriteLine(location.Name);
            Console.WriteLine(location.Lat);
            Console.WriteLine(location.Lng);

            return Json(new{ success = true});
        }

        private void setSessionViewData()
        {
            ViewData["Username"] = HttpContext.Session.GetString("UserName");
            ViewData["UserId"] = (int)HttpContext.Session.GetInt32("UserId");
        }

        public bool isLoggedIn(){
            int? UserId = HttpContext.Session.GetInt32("UserId");
            return (UserId != null);
        }

    }
}
