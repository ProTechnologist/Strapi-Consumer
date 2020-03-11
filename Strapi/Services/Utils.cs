using Microsoft.AspNetCore.Http;
using Strapi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Strapi.Services
{
    public class Utils
    {
        private IHttpContextAccessor httpContextAccessor; 
        public Utils(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        public User getUserInformation()
        {
            User user = new User();
            var identity = httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;

                user.username = claims?.FirstOrDefault(x => x.Type.Equals("user_name", StringComparison.OrdinalIgnoreCase))?.Value;
                user.email = claims?.FirstOrDefault(x => x.Type.Equals("user_email", StringComparison.OrdinalIgnoreCase))?.Value;

                user.provider = claims?.FirstOrDefault(x => x.Type.Equals("user_provider", StringComparison.OrdinalIgnoreCase))?.Value;
                user.confirmed = Convert.ToBoolean(claims?.FirstOrDefault(x => x.Type.Equals("user_confirmed", StringComparison.OrdinalIgnoreCase))?.Value);

                user.blocked = Convert.ToBoolean(claims?.FirstOrDefault(x => x.Type.Equals("user_blocked", StringComparison.OrdinalIgnoreCase))?.Value);
                user.role = new Role() { name = claims?.FirstOrDefault(x => x.Type.Equals("user_role", StringComparison.OrdinalIgnoreCase))?.Value }; 
            }
            return user;
        }
    }
}
