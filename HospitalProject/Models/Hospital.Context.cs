﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HospitalProject.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class HospitalContext : DbContext
    {
        public HospitalContext()
            : base("name=HospitalContext")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<EmergencyPatient> EmergencyPatients { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<PastPatient> PastPatients { get; set; }
        public DbSet<EmergencyDoctor> EmergencyDoctors { get; set; }
        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<Entrance> Entrances { get; set; }
        public DbSet<ParkingLot> ParkingLots { get; set; }
        public DbSet<AdminUser> AdminUsers { get; set; }
    }
}
