using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DASL_Import_App.Models
{
    public class School
    {
        public int ID { get; set; }

        public string RefId { get; set; }
        public string LocalId { get; set; }
        public string StateProvinceId { get; set; }
        public string SchoolName { get; set; }
        public string DistrictRefId { get; set; }
        public string SchoolUrl { get; set; }
        public string PrincipalName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string PhoneNumber { get; set; }
    }
}