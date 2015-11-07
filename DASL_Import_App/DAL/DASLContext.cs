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

        public DbSet<District> Districts { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Student> Students { get; set; }
    }
}