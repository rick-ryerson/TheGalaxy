﻿// <auto-generated />
using System;
using GalacticSenate.Data.Implementations.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GalacticSenate.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230324050122_PartyRoles")]
    partial class PartyRoles
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GalacticSenate.Domain.Model.Gender", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Value")
                        .IsUnique();

                    b.ToTable("Genders");
                });

            modelBuilder.Entity("GalacticSenate.Domain.Model.MaritalStatusType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Value")
                        .IsUnique();

                    b.ToTable("MaritalStatusTypes");
                });

            modelBuilder.Entity("GalacticSenate.Domain.Model.Organization", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PartyId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PartyId")
                        .IsUnique();

                    b.ToTable("Organizations");
                });

            modelBuilder.Entity("GalacticSenate.Domain.Model.OrganizationName", b =>
                {
                    b.Property<Guid>("OrganizationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("OrganizationNameValueId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FromDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ThruDate")
                        .HasColumnType("datetime2");

                    b.HasKey("OrganizationId", "OrganizationNameValueId", "FromDate");

                    b.HasIndex("OrganizationNameValueId");

                    b.HasIndex("OrganizationId", "OrganizationNameValueId", "FromDate", "ThruDate")
                        .IsUnique()
                        .HasFilter("[ThruDate] IS NOT NULL");

                    b.ToTable("OrganizationNames");
                });

            modelBuilder.Entity("GalacticSenate.Domain.Model.OrganizationNameValue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Value")
                        .IsUnique()
                        .HasFilter("[Value] IS NOT NULL");

                    b.ToTable("OrganizationNameValues");
                });

            modelBuilder.Entity("GalacticSenate.Domain.Model.Party", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Parties");
                });

            modelBuilder.Entity("GalacticSenate.Domain.Model.PartyRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("From")
                        .HasColumnType("datetime2");

                    b.Property<int?>("PartyRoleTypeId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Thru")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("PartyRoleTypeId");

                    b.ToTable("PartyRoles");
                });

            modelBuilder.Entity("GalacticSenate.Domain.Model.Person", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("PartyId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PartyId")
                        .IsUnique();

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("GalacticSenate.Domain.Model.PersonGender", b =>
                {
                    b.Property<Guid>("PersonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("GenderId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FromDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ThruDate")
                        .HasColumnType("datetime2");

                    b.HasKey("PersonId", "GenderId", "FromDate");

                    b.HasIndex("GenderId");

                    b.HasIndex("PersonId", "GenderId", "FromDate", "ThruDate")
                        .IsUnique()
                        .HasFilter("[ThruDate] IS NOT NULL");

                    b.ToTable("PersonGenders");
                });

            modelBuilder.Entity("GalacticSenate.Domain.Model.PersonMaritalStatus", b =>
                {
                    b.Property<Guid>("PersonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("MaritalStatusId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FromDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ThruDate")
                        .HasColumnType("datetime2");

                    b.HasKey("PersonId", "MaritalStatusId", "FromDate");

                    b.HasIndex("MaritalStatusId");

                    b.HasIndex("PersonId", "MaritalStatusId", "FromDate", "ThruDate")
                        .IsUnique()
                        .HasFilter("[ThruDate] IS NOT NULL");

                    b.ToTable("PersonMaritalStatuses");
                });

            modelBuilder.Entity("GalacticSenate.Domain.Model.PersonName", b =>
                {
                    b.Property<Guid>("PersonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("PersonNameValueId")
                        .HasColumnType("int");

                    b.Property<int>("PersonNameTypeId")
                        .HasColumnType("int");

                    b.Property<int>("Ordinal")
                        .HasColumnType("int");

                    b.Property<DateTime>("FromDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ThruDate")
                        .HasColumnType("datetime2");

                    b.HasKey("PersonId", "PersonNameValueId", "PersonNameTypeId", "Ordinal", "FromDate");

                    b.HasIndex("PersonNameTypeId");

                    b.HasIndex("PersonNameValueId");

                    b.HasIndex("PersonId", "PersonNameValueId", "PersonNameTypeId", "Ordinal", "FromDate", "ThruDate")
                        .IsUnique()
                        .HasFilter("[ThruDate] IS NOT NULL");

                    b.ToTable("PersonNames");
                });

            modelBuilder.Entity("GalacticSenate.Domain.Model.PersonNameType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Value")
                        .IsUnique();

                    b.ToTable("PersonNameTypes");
                });

            modelBuilder.Entity("GalacticSenate.Domain.Model.PersonNameValue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Value")
                        .IsUnique()
                        .HasFilter("[Value] IS NOT NULL");

                    b.ToTable("PersonNameValues");
                });

            modelBuilder.Entity("GalacticSenate.Domain.Model.RoleType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Description")
                        .IsUnique();

                    b.ToTable("RoleTypes");

                    b.HasDiscriminator<string>("Discriminator").HasValue("RoleType");
                });

            modelBuilder.Entity("GalacticSenate.Domain.Model.PartyRoleType", b =>
                {
                    b.HasBaseType("GalacticSenate.Domain.Model.RoleType");

                    b.Property<int?>("PartyRoleTypeId")
                        .HasColumnType("int");

                    b.HasIndex("PartyRoleTypeId");

                    b.HasDiscriminator().HasValue("PartyRoleType");
                });

            modelBuilder.Entity("GalacticSenate.Domain.Model.Organization", b =>
                {
                    b.HasOne("GalacticSenate.Domain.Model.Party", "Party")
                        .WithMany()
                        .HasForeignKey("PartyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Party");
                });

            modelBuilder.Entity("GalacticSenate.Domain.Model.OrganizationName", b =>
                {
                    b.HasOne("GalacticSenate.Domain.Model.Organization", "Organization")
                        .WithMany("Names")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GalacticSenate.Domain.Model.OrganizationNameValue", "OrganizationNameValue")
                        .WithMany()
                        .HasForeignKey("OrganizationNameValueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organization");

                    b.Navigation("OrganizationNameValue");
                });

            modelBuilder.Entity("GalacticSenate.Domain.Model.Party", b =>
                {
                    b.HasOne("GalacticSenate.Domain.Model.PartyRole", "Role")
                        .WithMany("Parties")
                        .HasForeignKey("RoleId");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("GalacticSenate.Domain.Model.PartyRole", b =>
                {
                    b.HasOne("GalacticSenate.Domain.Model.PartyRoleType", "PartyRoleType")
                        .WithMany()
                        .HasForeignKey("PartyRoleTypeId");

                    b.Navigation("PartyRoleType");
                });

            modelBuilder.Entity("GalacticSenate.Domain.Model.Person", b =>
                {
                    b.HasOne("GalacticSenate.Domain.Model.Party", "Party")
                        .WithMany()
                        .HasForeignKey("PartyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Party");
                });

            modelBuilder.Entity("GalacticSenate.Domain.Model.PersonGender", b =>
                {
                    b.HasOne("GalacticSenate.Domain.Model.Gender", "Gender")
                        .WithMany()
                        .HasForeignKey("GenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GalacticSenate.Domain.Model.Person", "Person")
                        .WithMany("Genders")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Gender");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("GalacticSenate.Domain.Model.PersonMaritalStatus", b =>
                {
                    b.HasOne("GalacticSenate.Domain.Model.MaritalStatusType", "MaritalStatus")
                        .WithMany()
                        .HasForeignKey("MaritalStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GalacticSenate.Domain.Model.Person", "Person")
                        .WithMany("MaritalStatuses")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MaritalStatus");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("GalacticSenate.Domain.Model.PersonName", b =>
                {
                    b.HasOne("GalacticSenate.Domain.Model.Person", "Person")
                        .WithMany("Names")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GalacticSenate.Domain.Model.PersonNameType", "PersonNameType")
                        .WithMany()
                        .HasForeignKey("PersonNameTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GalacticSenate.Domain.Model.PersonNameValue", "PersonNameValue")
                        .WithMany()
                        .HasForeignKey("PersonNameValueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");

                    b.Navigation("PersonNameType");

                    b.Navigation("PersonNameValue");
                });

            modelBuilder.Entity("GalacticSenate.Domain.Model.PartyRoleType", b =>
                {
                    b.HasOne("GalacticSenate.Domain.Model.PartyRoleType", null)
                        .WithMany("Roles")
                        .HasForeignKey("PartyRoleTypeId");
                });

            modelBuilder.Entity("GalacticSenate.Domain.Model.Organization", b =>
                {
                    b.Navigation("Names");
                });

            modelBuilder.Entity("GalacticSenate.Domain.Model.PartyRole", b =>
                {
                    b.Navigation("Parties");
                });

            modelBuilder.Entity("GalacticSenate.Domain.Model.Person", b =>
                {
                    b.Navigation("Genders");

                    b.Navigation("MaritalStatuses");

                    b.Navigation("Names");
                });

            modelBuilder.Entity("GalacticSenate.Domain.Model.PartyRoleType", b =>
                {
                    b.Navigation("Roles");
                });
#pragma warning restore 612, 618
        }
    }
}
