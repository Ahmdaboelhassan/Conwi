using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helper
{
    public class EmailSettings
    {
        public string email { get; set; }
        public string password { get; set; }
        public string host { get; set; }
        public int port { get; set; }
        public string displayName { get; set; }
    }
}