using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DASL_Import_App.Models
{
    public class District
    {
        public int ID { get; set; }

        public string RefId { get; set; }
        public string LocalId { get; set; }
        public string StateProvinceId { get; set; }
        public string LeaName { get; set; }
        public string LeaUrl { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
    }
}