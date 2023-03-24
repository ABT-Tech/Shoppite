using System;
using System.Collections.Generic;
using System.Text;

namespace Shoppite.Application.Models
{
    public class LoginCheckModel
    {
        public Loginsubmodel datal { get; set; }
    }
    public class Loginsubmodel
    {
        public string userid { get; set; }
        public string password { get; set; }
    }
}
