using database.models;
using Microsoft.EntityFrameworkCore;

namespace database.extensions
{
  public static class EntityExtensions
  {
    public static ModelBuilder medicineEntityExt(this ModelBuilder modelBuilder)
    {
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

             entity.HasMany(m => m.MedicineBenefits)
          .WithMany(b => b.Medicines)
          .UsingEntity<Dictionary<string, object>>(
              "MedicineBenefits", // Name of the join table
              j => j.HasOne<MedicineBenefits>().WithMany().HasForeignKey("BenefitId"),

              j => j.HasOne<Medicines>().WithMany().HasForeignKey("MedicineId"),

               j =>
              {
                j.HasKey("MedicineId", "BenefitId"); // Composite key
                j.ToTable("MedicineBenefits");
              });
             entity.HasMany(m => m.MedicalSideEffects)
                      .WithMany(b => b.Medicines)
                      .UsingEntity<Dictionary<string, object>>(
                          "MedicineSideEffects", // Name of the join table
                            j => j.HasOne<MedicalSideEffects>().WithMany().HasForeignKey("SideEffectId"),
                          j => j.HasOne<Medicines>().WithMany().HasForeignKey("MedicineId"),

                           j =>
                          {
                            j.HasKey("MedicineId", "SideEffectId"); // Composite key
                            j.ToTable("MedicineSideEffects");
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
  }

}