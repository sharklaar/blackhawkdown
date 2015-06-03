using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BhdResponsiveSite.Models
{
    public class EmailSendingModel
    {
        public string EmailType { get; set; }
        public List<string> EmailList { get; set; } 
    }
}