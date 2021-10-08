using Microsoft.EntityFrameworkCore;
using PeopleAndOrganizations.Domain.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace PeopleAndOrganizations.Data.Implementations.EntityFramework
{
    public class DataContext : DbContext
    {
        //public DataContext([NotNullAttribute] DbContextOptions options) : base(options)
        //{

        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost,14331;Database=Celestial;User Id=sa;Password=xdr5cft6!@!;");
        }

        public DbSet<Gender> Genders { get; set; }
        public DbSet<MaritalStatus> MaritalStatuses { get; set; }
        public DbSet<NameType> NameTypes { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        //public DbSet<OrganizationName> OrganizationNames { get; set; }
        //public DbSet<OrganizationNameValue> OrganizationNameValues { get; set; }
        //public DbSet<Party> Parties { get; set; }
        //public DbSet<Person> Persons { get; set; }
        //public DbSet<PersonGender> PersonGenders { get; set; }
        //public DbSet<PersonMaritalStatus> PersonMaritalStatuses { get; set; }
        //public DbSet<PersonName> PersonNames { get; set; }
        //public DbSet<PersonNameValue> PersonNameValues { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            BuildGenders(modelBuilder);
            BuildMartialStatuses(modelBuilder);
            BuildNameTypes(modelBuilder);
            BuildOrganizations(modelBuilder);
            //BuildOrganizationNameValues(modelBuilder);
            //BuildParties(modelBuilder);
            //BuildPersons(modelBuilder);
            //BuilddPersonGenders(modelBuilder);
            //BuildPersonMaritalStatues(modelBuilder);
            //BuildPersonNames(modelBuilder);
            //BuildPersonNameValues(modelBuilder);
        }

        private void BuildPersonNameValues(ModelBuilder modelBuilder)
        {
            throw new NotImplementedException();
        }

        private void BuildPersonNames(ModelBuilder modelBuilder)
        {
            throw new NotImplementedException();
        }

        private void BuildPersonMaritalStatues(ModelBuilder modelBuilder)
        {
            throw new NotImplementedException();
        }

        private void BuilddPersonGenders(ModelBuilder modelBuilder)
        {
            throw new NotImplementedException();
        }

        private void BuildPersons(ModelBuilder modelBuilder)
        {
            throw new NotImplementedException();
        }

        private void BuildParties(ModelBuilder modelBuilder)
        {
            throw new NotImplementedException();
        }

        private void BuildOrganizationNameValues(ModelBuilder modelBuilder)
        {
            throw new NotImplementedException();
        }

        private void BuildOrganizations(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Organization>()
                .HasKey(o => o.Id);

        }

        private void BuildNameTypes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NameType>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<NameType>()
                .HasIndex(g => g.Value)
                .IsUnique();

            modelBuilder.Entity<NameType>()
                .Property(p => p.Value)
                .IsRequired(true);
        }

        private void BuildMartialStatuses(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MaritalStatus>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<MaritalStatus>()
                .HasIndex(g => g.Value)
                .IsUnique();

            modelBuilder.Entity<MaritalStatus>()
                .Property(p => p.Value)
                .IsRequired(true);
        }

        private void BuildGenders(ModelBuilder modelBuilder)
        {
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
