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
    public class HomeController : Controller
    {
        private VolunteerContext _context;
        private string _controller;
        private string _action;

        public HomeController(VolunteerContext context)
        {
            _context = context;
            _controller = "User";
            _action = "Login";
        }

        [HttpGet]
        [Route("AllEvents")]
        public IActionResult AllEvents()
        {
            if (isLoggedIn())
            {
                setSessionViewData();

                List<Event> allEvents = _context.events.Include(EVENT => EVENT.Coordinator).Include(eventt => eventt.EventVolunteers).ThenInclude(eVolunteer => eVolunteer.User).ToList();
                return View("AllEvents", allEvents);
            }
            return RedirectToAction("Login", "User");
        }

        [HttpPost]
        [Route("CreateEvent")]
        public IActionResult CreateEvent(Event eventInfo) {
            if (isLoggedIn()) {

                if (ModelState.IsValid)
                {
                    setSessionViewData();
                    
                    User user = _context.users.SingleOrDefault(u => u.UserId == (int)ViewData["UserId"]);

                    eventInfo.Coordinator = user; 
                    _context.events.Add(eventInfo);
                    _context.event_volunteers.Add(new EventVolunteer{  EventId = eventInfo.EventId, UserId = (int)ViewData["UserId"] });
                    _context.SaveChanges();

                    return RedirectToAction("CreateLocation", "Create", new { EventId = eventInfo.EventId }); 
                    // return RedirectToAction("Dashboard", "Details", new { id = eventInfo.EventId }); 
                    // Dashboard.cshtml, View Details, Controller Details (Mark's)                
                }
            } else {
                return RedirectToAction(_action, _controller);
            }
            return View("New", eventInfo);
        }



        [HttpGet]
        [Route("DeleteEvent/{eventId}")]
        public IActionResult DeleteEvent(int eventId){
            if(isLoggedIn()){
                setSessionViewData();

                Event EVENT = _context.events.SingleOrDefault(e => e.EventId == eventId);

                if(EVENT != null){
                    if(EVENT.CoordinatorId == (int)ViewData["UserId"]){
                        _context.events.Remove(EVENT);   
                        _context.SaveChanges();
                    }
                }
                return RedirectToAction("AllEvents");
            }else{
                return RedirectToAction(_action, _controller);
            }
        }

        [HttpGet]
        [Route("Dummy/{eventId}")]
        public IActionResult Dummy(int eventId)
        {
            return View("Dummy");
        }

        [Route("{*url}")]
        public IActionResult Error()
        {
            Response.StatusCode = 404;
            
            return View(new ErrorViewModel{
                RequestId = "404"
            });
        }

        [HttpGet]
        [Route("Home")]
        public IActionResult Home()
        {
            if(isLoggedIn()){
                setSessionViewData();
                
                return View();
            }else{
                return RedirectToAction(_action, _controller);
            }
        }

        [HttpGet]
        [Route("JoinLeave/{eventId}/{location}")]
        public IActionResult JoinLeave(int eventId, string location)
        {
            if (isLoggedIn())
            {
                setSessionViewData();
                int userId = (int)ViewData["UserId"];

                EventVolunteer eventJoin = _context.event_volunteers.Where(eVolunteer => eVolunteer.EventId == eventId).SingleOrDefault(u => u.UserId == userId);

                if(eventJoin != null)
                {
                    _context.event_volunteers.Remove(eventJoin);
                } else {
                    _context.event_volunteers.Add(new EventVolunteer{ EventId = eventId, UserId =  userId});
                }
                _context.SaveChanges();
                return RedirectToAction(location);
            }
            return RedirectToAction(_action, _controller);
        }

        [HttpGet]
        [Route("New")]
        public IActionResult New()
        {
            if (isLoggedIn()) {
                return View(new Event());
            }
            return RedirectToAction(_action, _controller);
        }

        public void setSessionViewData()
        {
            ViewData["Username"] = HttpContext.Session.GetString("UserName");
            ViewData["UserId"] = (int)HttpContext.Session.GetInt32("UserId");
            ViewData["WholeUser"] = _context.users.SingleOrDefault(u => u.UserId == (int)ViewData["UserId"]);
        }

        public bool isLoggedIn(){
            int? UserId = HttpContext.Session.GetInt32("UserId");
            return (UserId != null);
        }

    }
}
