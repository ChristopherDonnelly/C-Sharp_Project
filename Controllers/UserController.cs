﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using C_Sharp_Project.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace C_Sharp_Project.Controllers
{
    public class UserController : Controller
    {
        private VolunteerContext _context;
        private string _controller;
        private string _action;

        public UserController(VolunteerContext context)
        {
            _context = context;
            _controller = "Create";
            _action = "CreateLocation";
        }

        [HttpGet]
        [Route("")]
        [Route("Login")]
        public IActionResult Login()
        {
            if(isLoggedIn()){
                return RedirectToAction(_action, _controller);
            }else{
                return View("LogReg", new LogRegBundleModel());
            }
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginViewModel model)
        {
			User user = _context.users.SingleOrDefault(u => u.Email == model.LoginEmail);
            model.Found = (user==null)?1:0;

            if(model.Found == 0){
                if(model.LoginPassword==null) model.LoginPassword=" ";

                var Hasher = new PasswordHasher<User>();
                model.LoginPasswordConfirmation = (Hasher.VerifyHashedPassword(user, user.Password, model.LoginPassword) != 0)?0:1;
            }

            TryValidateModel(model);

            if(ModelState.IsValid)
            {
                saveSession(user);
                return RedirectToAction(_action, _controller);
            }

            return View("LogReg", new LogRegBundleModel{ Login = model });
        }

        [HttpGet]
        [Route("Register")]
        public IActionResult Register()
        {
            if(isLoggedIn()){
                return RedirectToAction(_action, _controller);
            }else{
                return View("LogReg", new LogRegBundleModel());
            }
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                model.Unique = _context.users.Count(u => u.Email == model.Email);
                TryValidateModel(model);

                if(ModelState.IsValid){
                    saveSession(model.createUser(_context));
                    return RedirectToAction(_action, _controller);
                }
            }

            return View("LogReg", new LogRegBundleModel{ Register = model });
        }

        [HttpGet]
        [Route("Logoff")]
        public IActionResult Logoff()
        {
            HttpContext.Session.Clear();
            return View("LogReg", new LogRegBundleModel());
        }

        public void saveSession(User user){
            HttpContext.Session.SetInt32("UserId", (int)user.UserId);
            HttpContext.Session.SetString("UserName", (string)user.FirstName);
        }

        public bool isLoggedIn(){
            int? UserId = HttpContext.Session.GetInt32("UserId");
            return (UserId != null);
        }

    }
}
