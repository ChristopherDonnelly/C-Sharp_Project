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
