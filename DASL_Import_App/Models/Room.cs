using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DASL_Import_App.Models
{
    public class Room
    {
        public int ID { get; set; }

        public string RefId { get; set; }
        public string SchoolRefId { get; set; }
        public string StaffRefId { get; set; }
        public string RoomNumber { get; set; }
        public string Capacity { get; set; }
    }
}