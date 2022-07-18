using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace Chess_master_2.Models
{
    public class Online_Chess
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email_ID { get; set; }
        
        public string Country { get; set; }
        public int x1 { get; set; }
        public int NoResult { get; set; }
        //public bool IsGoogleLogin { get; set; }
    }
}