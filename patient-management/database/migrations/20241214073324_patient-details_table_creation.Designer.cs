﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using patient_management.database;

#nullable disable

namespace prescription_management.database.migrations
{
    [DbContext(typeof(postgresHealthCareDbContext))]
    [Migration("20241214073324_patient-details_table_creation")]
    partial class patientdetails_table_creation
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("patient_management.database.models.PatientDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("PatientAddress")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("patient_address");

                    b.Property<string>("PatientContact")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("patient_contact");

                    b.Property<DateTime>("PatientLatestDateOfVisit")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("patient_latest_date_of_visit");

                    b.Property<string>("PatientName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("patient_name");

                    b.Property<DateTime>("PatientRegistrationDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("patient_registration_date");

                    b.Property<string>("PatientUniqueId")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("patient_unique_id");

                    b.HasKey("Id");

                    b.ToTable("patient-details");
                });
#pragma warning restore 612, 618
        }
    }
}
