using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BHDSite.Models
{
    public class ContactViewModel
    {          
        [Required]
        [DataType(DataType.EmailAddress)]
        public string From { get; set; }
 
        [Required]
        public string Subject { get; set; }
 
        [Required]
        public string Message { get; set; }    
    }
}