using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DASL_Import_App.Models
{
    public class Absence
    {
        public int ID { get; set; }

        public Student Student { get; set; }
        public DateTime Date { get; set; }
    }
}