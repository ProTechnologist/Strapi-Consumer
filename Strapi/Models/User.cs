using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Strapi.Models
{
    public class User
    {
        public int id { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string provider { get; set; }
        public bool confirmed { get; set; }
        public bool blocked { get; set; }
        public Role role { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string date_string { get; set; }
    }
}
