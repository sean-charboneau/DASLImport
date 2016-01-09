using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DASL_Import_App.Models
{
    public class Class
    {
        public int ID { get; set; }

        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public string SectionNumber { get; set; }
        public string TermCode { get; set; }
        public string Location { get; set; }
        public string TeacherCode { get; set; }
        public string PeriodNumber { get; set; }
        public string ExternalRefId { get; set; }
    }
}