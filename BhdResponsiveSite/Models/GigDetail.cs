using System;
using System.Collections.Generic;

namespace BhdResponsiveSite.Models
{
    public class GigList
    {
        public List<GigDetail> Gigs { get; set; }
    }
    public class GigDetail
    {
        public DateTime GigDate { get; set; }
        public string Venue { get; set; }
        public string Location { get; set; }
        public string LinkUrl { get; set; }
        public string ExtraDetail { get; set; }

        public int Year => GigDate.Year;
        public int Month => GigDate.Month;
        public int Date => GigDate.Day;
    }
}