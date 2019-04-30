using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace SchedulerWebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Home()
        {
            return View();
        }

      
        public IActionResult Login(string returnUrl)
        {
            return Challenge(new AuthenticationProperties { RedirectUri = "/auth/signin" },
                             OpenIdConnectDefaults.AuthenticationScheme);
        }
    }
}