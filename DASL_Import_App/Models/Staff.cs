using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DASL_Import_App.Models
{
    public class Staff
    {
        public int ID { get; set; }

        public string RefId { get; set; }
        public string LocalId { get; set; }
        public string StateProvinceId { get; set; }
        public string SchoolRefId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}