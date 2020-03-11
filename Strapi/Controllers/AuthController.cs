using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Strapi.Models;
using Strapi.Services;

namespace Strapi.Controllers
{
    public class AuthController : Controller
    {
        public IConfiguration Configuration { get; set; }
        private IHttpBase _httpBase { get; set; }
        public AuthController(IConfiguration configuration, IHttpBase httpBase)
        {
            Configuration = configuration;
            _httpBase = httpBase;
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Users");
            }
            else
            {
                return View();
            }
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Validate(UserModel model)
        {
            if (ModelState.IsValid)
            {
                var input_data = new
                {
                    identifier = model.UserName,
                    password = model.Password
                };

                var json = JsonConvert.SerializeObject(input_data);
                var url = Configuration["ApiUrl"].ToString();

                var userInfo = await _httpBase.Post(url, json);
                LoginResp obj = JsonConvert.DeserializeObject<LoginResp>(userInfo);

                if (!string.IsNullOrEmpty(obj.jwt))
                {
                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, obj.user.username),
                    new Claim(ClaimTypes.Role, obj.user.role.name),
                    new Claim("user_name", obj.user.username),
                    new Claim("user_email", obj.user.email),
                    new Claim("user_provider",obj.user.provider.ToString()),
                    new Claim("user_confirmed",obj.user.confirmed.ToString()),
                    new Claim("user_blocked",obj.user.blocked.ToString()),
                    new Claim("user_role",obj.user.role.name.ToString()),
                    new Claim("j_token",obj.jwt)
                };

                    var claimsIdentityName = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var claimsIdentityRole = new ClaimsIdentity(
                       claims, CookieAuthenticationDefaults.AuthenticationScheme, obj.user.role.name, ClaimTypes.Role);

                    List<ClaimsIdentity> claimsIdentities = new List<ClaimsIdentity>();
                    claimsIdentities.Add(claimsIdentityName);
                    claimsIdentities.Add(claimsIdentityRole);


                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true,
                        IssuedUtc = DateTime.Now
                    };

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentityName),
                        authProperties);

                    return RedirectToAction("Index", "Home");
                }

                else
                { return RedirectToAction("Login", "Auth"); }
            }
            return View();

        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}