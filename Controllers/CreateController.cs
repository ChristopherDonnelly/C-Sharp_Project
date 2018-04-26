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
        [Route("CreateLocation")]
        public IActionResult CreateLocation()
        {
            if(isLoggedIn()){
                setSessionViewData();
                
                // _context.events.Include(e => e.EventVolunteers).ThenInclude(u => u.User).Where().ToList();

                // User user = _context.users.Include( e => e.Events ).ThenInclude( j => j.Event ).SingleOrDefault(u => u.UserId == (int)ViewData["UserId"]);
                // List<Event> events = _context.events.Include( j => j.EventVolunteers ).ThenInclude( u => u.User ).ToList();

                // Console.WriteLine("*************************************");
                // for(int i=0; i<user.Events.Count; i++){
                //     Console.WriteLine(" Joined Event Name: " + user.Events[i].Event.Name);
                // }
                // foreach(Event e in events)
                // {
                //     Console.WriteLine(" Event Name: "+ e.Name);

                //     for(int j=0; j<e.EventVolunteers.Count; j++){
                //         Console.WriteLine(" Volunteer Name: "+ e.EventVolunteers[j].User.FirstName);
                //     }
                // }
                // Console.WriteLine("*************************************");

                return View();
            }else{
                return RedirectToAction(_action, _controller);
            }
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
