using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BhdResponsiveSite.Models
{
    public class GigDetail
    {
        public DateTime GigDate { get; set; }
        public string Venue { get; set; }
        public string Location { get; set; }
        public string LinkUrl { get; set; }
        public string ExtraDetail { get; set; }

    }
}