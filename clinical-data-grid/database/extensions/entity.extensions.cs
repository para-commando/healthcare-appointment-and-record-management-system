using clinical_data_grid.database.models;
using Microsoft.EntityFrameworkCore;

namespace clinical_data_grid.database.extensions;
public static class EntityExtensions
{
  public static ModelBuilder medicineEntityExt(this ModelBuilder modelBuilder)
  {

    // this is fluent api use case implementation and is higher in precedence compared to data annotations
    modelBuilder.Entity<Medicines>(entity =>
         {
           entity.ToTable("medicines"); // Map to table name in the database
           entity.HasKey(m => m.id);   // Specify primary key
           entity.Property(m => m.name)
                  .HasColumnName("medicine_name") // Specify column name
                  .IsRequired()                   // Mark as required
                  .HasMaxLength(255);             // Set max length

           entity.Property(m => m.details)
                  .HasColumnName("medicine_details")
                  .HasMaxLength(255);


           // these below lines even if commented out, the join table will be created as in model file associations has been defined

           entity.HasMany(m => m.MedicineBenefits)
        .WithMany(b => b.Medicines)
        .UsingEntity<Dictionary<string, object>>(
            "medicine_benefits_join", // Name of the join table
            j => j.HasOne<MedicineBenefits>().WithMany().HasForeignKey("BenefitId"),

            j => j.HasOne<Medicines>().WithMany().HasForeignKey("MedicineId"),

             j =>
            {
              j.HasKey("MedicineId", "BenefitId"); // Composite key
              j.ToTable("medicine_benefits_join");
            });
           entity.HasMany(m => m.MedicalSideEffects)
                    .WithMany(b => b.Medicines)
                    .UsingEntity<Dictionary<string, object>>(
                        "medicine_sideEffects_join", // Name of the join table
                          j => j.HasOne<MedicalSideEffects>().WithMany().HasForeignKey("SideEffectId"),
                        j => j.HasOne<Medicines>().WithMany().HasForeignKey("MedicineId"),

                         j =>
                        {
                          j.HasKey("MedicineId", "SideEffectId"); // Composite key
                          j.ToTable("medicine_sideEffects_join");
                        });



         });

    return modelBuilder;
  }
  public static ModelBuilder medicineBenefitsEntityExt(this ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<MedicineBenefits>(entity =>
         {
           entity.ToTable("medicine_benefits"); // Map to table name in the database
           entity.HasKey(m => m.id);   // Specify primary key
           entity.Property(m => m.benefit)
                  .HasColumnName("benefit") // Specify column name
                  .IsRequired()                   // Mark as required
                  .HasMaxLength(255);             // Set max length

         });

    return modelBuilder;
  }
  public static ModelBuilder medicineSideEffectsEntityExt(this ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<MedicalSideEffects>(entity =>
         {
           entity.ToTable("medical_side_effects"); // Map to table name in the database
           entity.HasKey(m => m.id);   // Specify primary key
           entity.Property(m => m.sideEffects)
                  .HasColumnName("side_effects") // Specify column name
                  .IsRequired()                   // Mark as required
                  .HasMaxLength(255);             // Set max length

         });

    return modelBuilder;
  }

  public static ModelBuilder diseasesEntityExt(this ModelBuilder modelBuilder)
  {

    // this is fluent api use case implementation and is higher in precedence compared to data annotations
    modelBuilder.Entity<Diseases>(entity =>
         {
           entity.ToTable("diseases"); // Map to table name in the database
           entity.HasKey(m => m.id);   // Specify primary key
           entity.Property(m => m.name)
                  .HasColumnName("name") // Specify column name
                  .IsRequired()                   // Mark as required
                  .HasMaxLength(255);

           // these below lines even if commented out, the join table will be created as in model file associations has been defined

           entity.HasMany(m => m.Symptoms)
        .WithMany(b => b.Diseases)
        .UsingEntity<Dictionary<string, object>>(
            "symptoms_diseases_join", // Name of the join table
            j => j.HasOne<Symptoms>().WithMany().HasForeignKey("SymptomsId"),

            j => j.HasOne<Diseases>().WithMany().HasForeignKey("DiseasesId"),

             j =>
            {
              j.HasKey("DiseasesId", "SymptomsId"); // Composite key
              j.ToTable("symptoms_diseases_join");
            });

           entity.HasMany(m => m.Medicines)
      .WithMany(b => b.Diseases)
      .UsingEntity<Dictionary<string, object>>(
          "symptoms_medicine_join", // Name of the join table
          j => j.HasOne<Medicines>().WithMany().HasForeignKey("MedicinesId"),

          j => j.HasOne<Diseases>().WithMany().HasForeignKey("DiseasesId"),

           j =>
          {
            j.HasKey("DiseasesId", "MedicinesId"); // Composite key
            j.ToTable("symptoms_medicine_join");
          });

         });

    return modelBuilder;
  }
}

