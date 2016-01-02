using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DASL_Import_App.Models
{
    public class School
    {
        public int ID { get; set; }

        public string SchoolId { get; set; }
        public string DistrictSchoolId { get; set; }
        public string SchoolName { get; set; }
        public string PrincipalName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZIP { get; set; }
        public string PhoneNumber { get; set; }
        public string ExternalRefId { get; set; }
    }
}