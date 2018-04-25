using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using C_Sharp_Project.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

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


        [Route("{*url}")]
        public IActionResult Error()
        {
            Response.StatusCode = 404;
            
            return View(new ErrorViewModel{
                RequestId = "404"
            });
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
