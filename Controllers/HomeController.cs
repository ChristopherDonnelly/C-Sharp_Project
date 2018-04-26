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

                    return RedirectToAction("Dummy", new { eventId = eventInfo.EventId }); 
                    // Dashboard.cshtml, View Details, Controller Details (Mark's)                
                }
            } else {
                return RedirectToAction(_action, _controller);
            }
            return View("New", eventInfo);
        }



        [HttpGet]
        [Route("DeleteEvent/{EventId}")]
        public IActionResult DeleteEvent(int EventId){
            if(isLoggedIn()){
                setSessionViewData();

                // Ideas idea = _context.ideas.SingleOrDefault(u => u.IdeaId == IdeaId);

                // if(idea != null){
                //     if(idea.IdeaCreatorId == (int)ViewData["UserId"]){
                //         _context.ideas.Remove(idea);   
                //         _context.SaveChanges();
                //     }
                // }

                return RedirectToAction("Home");
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
        [Route("New")]
        public IActionResult New()
        {
            if (isLoggedIn()) {
                return View(new Event());
            }
            return RedirectToAction(_action, _controller);
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
