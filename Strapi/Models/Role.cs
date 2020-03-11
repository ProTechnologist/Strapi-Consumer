using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Strapi.Models
{
    public class Role
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string type { get; set; }
    }
}
