using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using DASL_Import_App.Models;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace DASL_Import_App.DAL
{
    public class DASLContext : DbContext
    {
        public DASLContext() : base("DASLContext")
        {
        }

        public DbSet<Absence> Absences { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<SchoolPeriod> SchoolPeriods { get; set; }
        public DbSet<StaffMember> StaffMembers { get; set; }
        public DbSet<StudentClass> StudentClasses { get; set; }
        public DbSet<Student> Students { get; set; }
    }
}