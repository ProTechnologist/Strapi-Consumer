using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Strapi.Models;

namespace Strapi.Controllers
{
    public class UsersController : Controller
    {
       [Authorize]
        public IActionResult Index()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                User model = new User();
                IEnumerable<Claim> claims = identity.Claims;
             
                model.username = claims?.FirstOrDefault(x => x.Type.Equals("user_name", StringComparison.OrdinalIgnoreCase))?.Value;
                model.email = claims?.FirstOrDefault(x => x.Type.Equals("user_email", StringComparison.OrdinalIgnoreCase))?.Value;
                model.date_string = claims?.FirstOrDefault(x => x.Type.Equals("created_date", StringComparison.OrdinalIgnoreCase))?.Value;
                return View(model);

            }
            return RedirectToAction("Login", "Auth");

        }
    }
}