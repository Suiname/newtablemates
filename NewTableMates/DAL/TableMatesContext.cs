using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using NewTableMates.Models;

namespace NewTableMates.DAL
{
    public class TableMatesContext : DbContext
    {
        public TableMatesContext() : base("TableMatesContext")
        {
        }

        public DbSet<Attendee> Attendees { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}