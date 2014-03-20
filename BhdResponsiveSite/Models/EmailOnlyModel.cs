using System.ComponentModel.DataAnnotations;

namespace BhdResponsiveSite.Models
{
    public class EmailOnlyModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}