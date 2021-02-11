using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EMP.DbScaffold.Models.STS
{
    public partial class stsContext : DbContext
    {
        public stsContext()
        {
        }

        public stsContext(DbContextOptions<stsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL(
                    "server=mycompany6921.mysql.database.azure.com;uid=appuser@mycompany6921;password=Soil9303;port=3306;database=sts;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // entity is EntityTypeBuilder
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => new { e.UserName, e.Password })
                    .HasName("PRIMARY");

                entity.ToTable("users");

                entity.HasIndex(e => new { e.UserName, e.Password })
                    .HasName("users")
                    .IsUnique();

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnName("user_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasColumnName("user_name")
                    .HasMaxLength(40);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(50);

                entity.Property(e => e.HashedPassword)
                    .HasColumnName("hashed_password")
                    .HasMaxLength(512);

                entity.Property(e => e.Locked)
                    .HasColumnName("locked")
                    .HasColumnType("bool");
            });
        }


        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
