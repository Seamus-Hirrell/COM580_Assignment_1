﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace assignment_1
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class VeterinaryPracticeModelContainer : DbContext
    {
        public VeterinaryPracticeModelContainer()
            : base("name=VeterinaryPracticeModelContainer")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Owner> Owners { get; set; }
        public virtual DbSet<Pet> Pets { get; set; }
        public virtual DbSet<Practice> Practices { get; set; }
        public virtual DbSet<Vet> Vets { get; set; }
        public virtual DbSet<Visit> Visits { get; set; }
        public virtual DbSet<Treatment> Treatments { get; set; }
        public virtual DbSet<Medication> Medications { get; set; }
    }
}