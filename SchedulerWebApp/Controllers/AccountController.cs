using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
namespace SchedulerWebApp.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            ClaimsPrincipal currentUser =  HttpContext.User;
            ViewBag.UserName = "None";
            foreach (var claim in currentUser.Claims)
            {
                ViewBag.UserName += claim.Value;
            }

            return View();
        }
    }
}