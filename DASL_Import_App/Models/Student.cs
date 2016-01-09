using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DASL_Import_App.Models
{
    public class Student
    {
        public int ID { get; set; }

        [Index(IsUnique = true)]
        public long StudentId { get; set; }
        public string School { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string GradeLevel { get; set; }
        public string ExternalRefId { get; set; }
    }
}
