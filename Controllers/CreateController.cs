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
            _context.locations.Add(location);
            _context.SaveChanges();

            return Json(new{ EventId = location.EventId, LocationId = location.LocationId });
        }

        [HttpPut]
        [Route("UpdateLocation")]
        public JsonResult UpdateLocation([FromBody] Location location)
        {
            Console.WriteLine("Location Id: "+ location.LocationId);
            Console.WriteLine("Location Lat: "+ location.Lat);
            Console.WriteLine("Location Lng: "+ location.Lng);

            Location dbLocation = _context.locations.SingleOrDefault(l => l.LocationId == location.LocationId);
            dbLocation.Lat = location.Lat;
            dbLocation.Lng = location.Lng;

            _context.SaveChanges();

            return Json(new{ Success = true });
        }

        [HttpDelete]
        [Route("DeleteLocation")]
        public JsonResult DeleteLocation([FromBody] Location location)
        {
            Location dbLocation = _context.locations.SingleOrDefault(l => l.LocationId == location.LocationId);

            _context.Remove(dbLocation);
            _context.SaveChanges();

            return Json(new{ Success = true });
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
