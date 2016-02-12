using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DASL_Import_App.Models
{
    public class StaffMember
    {
        public int ID { get; set; }

        [Index(IsUnique = true)]
        public long TeacherId { get; set; }
        public string DistrictStaffId { get; set; }
        public string SchoolId { get; set; }
        public string FirstName { get; set; }
        public string MidName { get; set; }
        public string LastName { get; set; }
        public string SchoolName { get; set; }
        public string SchoolShortName { get; set; }
        public string ExternalRefId { get; set; }
    }
}
