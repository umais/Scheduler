using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
namespace SchedulerWebApp.Controllers
{
    public class authController : Controller
    {
        [Authorize]
        public IActionResult signin()
        {
            return View();
        }
    }
}