using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Strapi.Services;

namespace Strapi.Controllers
{
    public class ProfileController : BaseController
    {
        public ProfileController(IHttpContextAccessor  httpContextAccessor): base(httpContextAccessor)
        {

        }
        public IActionResult Index()
        {
            //get user information through claims
            var userInfo =  new Utils(httpContextAccessor).getUserInformation();
            return View(userInfo);
        }
    }
}