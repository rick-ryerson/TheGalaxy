using Microsoft.EntityFrameworkCore;
using GalacticSenate.Domain.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace GalacticSenate.Data.Implementations.EntityFramework {
   public class DataContext : DbContext {
      public DataContext([NotNullAttribute] DbContextOptions options) : base(options) {

      }

      internal DataContext() {
      }

      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
         if (!optionsBuilder.IsConfigured) {
            optionsBuilder.UseSqlServer("Server=localhost,14331;Database=Celestial;User Id=sa;Password=qweasd!@!;");
         }
         // optionsBuilder.UseSqlServer("Server=localhost,14331;Database=Celestial;User Id=sa;Password=qweasd!@!;");
         base.OnConfiguring(optionsBuilder);
         
      }

      public DbSet<Gender> Genders { get; set; }
      public DbSet<MaritalStatusType> MaritalStatusTypes { get; set; }
      public DbSet<PersonNameType> PersonNameTypes { get; set; }
      public DbSet<Organization> Organizations { get; set; }
      public DbSet<OrganizationName> OrganizationNames { get; set; }
      public DbSet<OrganizationNameValue> OrganizationNameValues { get; set; }
      public DbSet<Party> Parties { get; set; }
      public DbSet<Person> Persons { get; set; }
      public DbSet<PersonGender> PersonGenders { get; set; }
      public DbSet<PersonMaritalStatus> PersonMaritalStatuses { get; set; }
      public DbSet<PersonName> PersonNames { get; set; }
      public DbSet<PersonNameValue> PersonNameValues { get; set; }

      protected override void OnModelCreating(ModelBuilder modelBuilder) {
         BuildGenders(modelBuilder);
         BuildMaritalStatusTypes(modelBuilder);
         BuildPersonNameTypes(modelBuilder);
         BuildOrganizations(modelBuilder);
         BuildOrganizationNameValues(modelBuilder);
         BuildOrganizationNames(modelBuilder);

         BuildParties(modelBuilder);
         BuildPersons(modelBuilder);
         BuilddPersonGenders(modelBuilder);
         BuildPersonMaritalStatuses(modelBuilder);
         BuildPersonNames(modelBuilder);
         BuildPersonNameValues(modelBuilder);
      }

      private void BuildPersonNameValues(ModelBuilder modelBuilder) {
         modelBuilder.Entity<PersonNameValue>()
            .HasKey(p => p.Id);
         modelBuilder.Entity<PersonNameValue>()
            .HasIndex(p => p.Value)
            .IsUnique(true);;
      }

      private void BuildPersonNames(ModelBuilder modelBuilder) {
         modelBuilder.Entity<PersonName>()
             .HasKey(p => new { p.PersonId, p.PersonNameValueId, p.PersonNameTypeId, p.Ordinal, p.FromDate });
         modelBuilder.Entity<PersonName>()
             .HasIndex(p => new { p.PersonId, p.PersonNameValueId, p.PersonNameTypeId, p.Ordinal, p.FromDate, p.ThruDate })
             .IsUnique(true);


         modelBuilder.Entity<PersonName>()
             .Property(p => p.Ordinal)
             .IsRequired(true);
         modelBuilder.Entity<PersonName>()
             .Property(p => p.PersonId)
             .IsRequired(true);
         modelBuilder.Entity<PersonName>()
             .Property(p => p.PersonNameValueId)
             .IsRequired(true);
         modelBuilder.Entity<PersonName>()
             .Property(p => p.PersonNameTypeId)
             .IsRequired(true);
         modelBuilder.Entity<PersonName>()
             .Property(p => p.FromDate)
             .IsRequired(true);
         modelBuilder.Entity<PersonName>()
             .Property(p => p.ThruDate)
             .IsRequired(false);
      }

      private void BuildPersonMaritalStatuses(ModelBuilder modelBuilder) {
         modelBuilder.Entity<PersonMaritalStatus>()
             .HasKey(p => new { p.PersonId, p.MaritalStatusId, p.FromDate });

         modelBuilder.Entity<PersonMaritalStatus>()
             .HasIndex(p => new { p.PersonId, p.MaritalStatusId, p.FromDate, p.ThruDate })
             .IsUnique(true);
      }

      private void BuilddPersonGenders(ModelBuilder modelBuilder) {
         modelBuilder.Entity<PersonGender>()
             .HasKey(p => new { p.PersonId, p.GenderId, p.FromDate });

         modelBuilder.Entity<PersonGender>()
             .HasIndex(p => new { p.PersonId, p.GenderId, p.FromDate, p.ThruDate })
             .IsUnique(true);
      }

      private void BuildPersons(ModelBuilder modelBuilder) {
         modelBuilder.Entity<Person>()
             .Property(p => p.Id)
             .ValueGeneratedOnAdd();

         modelBuilder.Entity<Person>()
             .HasIndex(p => p.PartyId)
             .IsUnique();
      }

      private void BuildParties(ModelBuilder modelBuilder) {
         modelBuilder.Entity<Party>()
            .HasKey(p => p.Id);
      }
      private void BuildOrganizationNames(ModelBuilder modelBuilder) {
         modelBuilder.Entity<OrganizationName>()
             .HasKey(p => new { p.OrganizationId, p.OrganizationNameValueId, p.FromDate });

         modelBuilder.Entity<OrganizationName>()
             .HasIndex(p => new { p.OrganizationId, p.OrganizationNameValueId, p.FromDate, p.ThruDate })
             .IsUnique();
         modelBuilder.Entity<OrganizationName>()
             .Property(p => p.OrganizationId)
             .IsRequired();
         modelBuilder.Entity<OrganizationName>()
             .Property(p => p.OrganizationNameValueId)
             .IsRequired();
         modelBuilder.Entity<OrganizationName>()
             .Property(p => p.FromDate)
             .IsRequired();
         modelBuilder.Entity<OrganizationName>()
             .Property(p => p.ThruDate)
             .IsRequired(false);
      }
      private void BuildOrganizationNameValues(ModelBuilder modelBuilder) {
         modelBuilder.Entity<OrganizationNameValue>()
             .Property(p => p.Id)
             .ValueGeneratedOnAdd();

         modelBuilder.Entity<OrganizationNameValue>()
             .HasIndex(p => p.Value)
             .IsUnique();

         modelBuilder.Entity<OrganizationNameValue>()
             .Property(p => p.Id)
             .IsRequired(true);
      }

      private void BuildOrganizations(ModelBuilder modelBuilder) {
         modelBuilder.Entity<Organization>()
             .HasKey(o => o.Id);

         modelBuilder.Entity<Organization>()
             .HasIndex(p => p.PartyId)
             .IsUnique(true);
      }

      private void BuildPersonNameTypes(ModelBuilder modelBuilder) {
         modelBuilder.Entity<PersonNameType>()
             .Property(p => p.Id)
             .ValueGeneratedOnAdd();

         modelBuilder.Entity<PersonNameType>()
             .HasIndex(g => g.Value)
             .IsUnique();

         modelBuilder.Entity<PersonNameType>()
             .Property(p => p.Value)
             .IsRequired(true);
      }

      private void BuildMaritalStatusTypes(ModelBuilder modelBuilder) {
         modelBuilder.Entity<MaritalStatusType>()
             .Property(p => p.Id)
             .ValueGeneratedOnAdd();

         modelBuilder.Entity<MaritalStatusType>()
             .HasIndex(g => g.Value)
             .IsUnique(true);

         modelBuilder.Entity<MaritalStatusType>()
             .Property(p => p.Value)
             .IsRequired(true);
      }

      private void BuildGenders(ModelBuilder modelBuilder) {
         modelBuilder.Entity<Gender>()
             .Property(p => p.Id)
             .ValueGeneratedOnAdd();

         modelBuilder.Entity<Gender>()
             .HasIndex(g => g.Value)
             .IsUnique();

         modelBuilder.Entity<Gender>()
             .Property(p => p.Value)
             .IsRequired(true);
      }
   }
}
