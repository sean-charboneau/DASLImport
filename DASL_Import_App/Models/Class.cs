using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DASL_Import_App.Models
{
    public class Class
    {
        public int ID { get; set; }

        public string DistrictCourseId { get; set; }
        public string CourseName { get; set; }
        public string SchoolId { get; set; }
        public string ClassId { get; set; }
        public string CourseId { get; set; }
        public string RoomId { get; set; }
        public string TeacherId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string ExternalRefId { get; set; }
    }
}