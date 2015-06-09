using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BhdResponsiveSite.Models
{
    public class EmailSendingResultsModel
    {
        public int Successes { get; set; }
        public int Failures { get; set; }
    }
}