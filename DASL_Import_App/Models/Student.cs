using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DASL_Import_App.Models
{
    public class Student
    {
        public int ID { get; set; }

        public string RefId { get; set; }
        public string LocalId { get; set; }
        public string StateProvinceId { get; set; }
        public string SchoolRefId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string HomeroomLocalId { get; set; }
        public string GradeLevel { get; set; }
    }
}