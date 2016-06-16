using System.Collections.Generic;

namespace BhdResponsiveSite.Models
{
    public class EmailSendingModel
    {
        public string EmailType { get; set; }
        public bool IsTestEmail { get; set; }
        public List<string> EmailList { get; set; } 
    }
}