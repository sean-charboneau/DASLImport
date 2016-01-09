using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DASL_Import_App.Models
{
    public class SchoolPeriod
    {
        public int ID { get; set; }

        public string School { get; set; }
        public string PeriodNumber { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}
