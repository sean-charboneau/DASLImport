using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DASL_Import_App.Models
{
    public class Student
    {
        public int ID { get; set; }

        public string StudentId { get; set; }
        public string SchoolId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string DOB { get; set; }
        public string GradeLevel { get; set; }
        public string ExternalRefId { get; set; }
    }
}
