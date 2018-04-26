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
    public class DetailsController : Controller
    {
        private VolunteerContext _context;
        private string _controller;
        private string _action;

        public DetailsController(VolunteerContext context)
        {
            _context = context;
            _controller = "User";
            _action = "Login";
        }

        [HttpGet]
        [Route("Dashboard/{id}")]
        public IActionResult Dashboard(int id)
        {
            if(isLoggedIn()){
                setSessionViewData();
                GetEventInfo(id);
                GetUserInfo();
                return View();
            }else{
                return RedirectToAction(_action, _controller);
            }
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


        private void setSessionViewData()
        {
            ViewData["Username"] = HttpContext.Session.GetString("UserName");
            ViewData["UserId"] = (int)HttpContext.Session.GetInt32("UserId");
        }

        public bool isLoggedIn(){
            int? UserId = HttpContext.Session.GetInt32("UserId");
            return (UserId != null);
        }

        public User GetUserInfo()
        {
            int UserId = (int)HttpContext.Session.GetInt32("UserId");
            User ConfirmedUser = _context.users.SingleOrDefault(User => User.UserId == UserId);
            ViewBag.ConfirmedUser = ConfirmedUser;
            return ConfirmedUser;
        }
        public Event GetEventInfo(int Id)
        {
            Event ConfirmedEvent = _context.events.Include(e => e.EventVolunteers).ThenInclude(u => u.User).Include(t => t.Tasks).ThenInclude(l => l.Loc).SingleOrDefault(ev => ev.EventId == Id);

            ViewBag.ConfirmedEvent = ConfirmedEvent;

            return ConfirmedEvent;
        }
    }
}
