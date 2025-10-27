using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Repository.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Part> Parts { get; set; }

    public virtual DbSet<PartInventory> PartInventories { get; set; }

    public virtual DbSet<Vehicle> Vehicles { get; set; }

    public virtual DbSet<VehiclePart> VehicleParts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("User Id=postgres.xfebkkgpoxmikxytlkcy;Password=Baconcho123@;Server=aws-1-ap-southeast-1.pooler.supabase.com;Port=6543;Database=postgres");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresEnum("auth", "aal_level", new[] { "aal1", "aal2", "aal3" })
            .HasPostgresEnum("auth", "code_challenge_method", new[] { "s256", "plain" })
            .HasPostgresEnum("auth", "factor_status", new[] { "unverified", "verified" })
            .HasPostgresEnum("auth", "factor_type", new[] { "totp", "webauthn", "phone" })
            .HasPostgresEnum("auth", "oauth_authorization_status", new[] { "pending", "approved", "denied", "expired" })
            .HasPostgresEnum("auth", "oauth_client_type", new[] { "public", "confidential" })
            .HasPostgresEnum("auth", "oauth_registration_type", new[] { "dynamic", "manual" })
            .HasPostgresEnum("auth", "oauth_response_type", new[] { "code" })
            .HasPostgresEnum("auth", "one_time_token_type", new[] { "confirmation_token", "reauthentication_token", "recovery_token", "email_change_token_new", "email_change_token_current", "phone_change_token" })
            .HasPostgresEnum("realtime", "action", new[] { "INSERT", "UPDATE", "DELETE", "TRUNCATE", "ERROR" })
            .HasPostgresEnum("realtime", "equality_op", new[] { "eq", "neq", "lt", "lte", "gt", "gte", "in" })
            .HasPostgresEnum("storage", "buckettype", new[] { "STANDARD", "ANALYTICS" })
            .HasPostgresExtension("extensions", "pg_stat_statements")
            .HasPostgresExtension("extensions", "pgcrypto")
            .HasPostgresExtension("extensions", "uuid-ossp")
            .HasPostgresExtension("graphql", "pg_graphql")
            .HasPostgresExtension("vault", "supabase_vault");

        modelBuilder.Entity<Part>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("parts_pkey");

            entity.ToTable("parts", "product_service");

            entity.HasIndex(e => e.Id, "parts_id_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.IsActive).HasColumnName("isactive");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.Type)
                .HasColumnType("character varying")
                .HasColumnName("type");
            entity.Property(e => e.WarrantyMoths).HasColumnName("warranty_moths");
        });

        modelBuilder.Entity<PartInventory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("part_inventory_pkey");

            entity.ToTable("part_inventory", "product_service");

            entity.HasIndex(e => e.Id, "part_inventory_id_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.ManufactureId).HasColumnName("manufacture_id");
            entity.Property(e => e.PartId).HasColumnName("part_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.ServiceCenterId).HasColumnName("service_center_id");

            entity.HasOne(d => d.Part).WithMany(p => p.PartInventories)
                .HasForeignKey(d => d.PartId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("part_inventory_part_id_fkey");
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("vehicles_pkey");

            entity.ToTable("vehicles", "product_service");

            entity.HasIndex(e => e.Id, "vehicles_id_key").IsUnique();

            entity.HasIndex(e => e.Vin, "vehicles_vin_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.ManufactureId).HasColumnName("manufacture_id");
            entity.Property(e => e.Model)
                .HasColumnType("character varying")
                .HasColumnName("model");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Vin)
                .HasColumnType("character varying")
                .HasColumnName("vin");
            entity.Property(e => e.WarrantyEnd).HasColumnName("warranty_end");
            entity.Property(e => e.WarrantyStart).HasColumnName("warranty_start");
            entity.Property(e => e.Year).HasColumnName("year");
        });

        modelBuilder.Entity<VehiclePart>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("vehicle_parts_pkey");

            entity.ToTable("vehicle_parts", "product_service");

            entity.HasIndex(e => e.Id, "vehicle_parts_id_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.IsActive).HasColumnName("isactive");
            entity.Property(e => e.PartId).HasColumnName("part_id");
            entity.Property(e => e.SerialNumber)
                .HasColumnType("character varying")
                .HasColumnName("serial_number");
            entity.Property(e => e.VehicleId).HasColumnName("vehicle_id");

            entity.HasOne(d => d.Part).WithMany(p => p.VehicleParts)
                .HasForeignKey(d => d.PartId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("vehicle_parts_part_id_fkey");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.VehicleParts)
                .HasForeignKey(d => d.VehicleId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("vehicle_parts_vehicle_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
