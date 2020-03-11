using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Strapi.Models
{
    public class LoginResp
    {
        public string jwt { get; set; }
        public User user { get; set; }
    }
}
