using System.ComponentModel.DataAnnotations;

namespace BhdResponsiveSite.Models
{
    public class ContactViewModel
    {          
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
 
        [Required]
        public string Subject { get; set; }
 
        [Required]
        public string Message { get; set; }    
    }
}