using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EMP.Sts.Models;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EMP.Sts.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // protected override void OnModelCreating(ModelBuilder builder)
        // {
        //     base.OnModelCreating(builder);
        //     // Customize the ASP.NET Identity model and override the defaults if needed.
        //     // For example, you can rename the ASP.NET Identity table names and more.
        //     // Add your customizations after calling base.OnModelCreating(builder);
        // }

        // OnModelCreating had to be overridden due to bugs in MySql.Data.EntityFrameworkCore 8.0.19
        // that raises "No coercion operator is defined between types 'System.Int16' and 'System.Boolean'"
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Iterate over every DbSet<> found in the current DbContext
            foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
            {
                // Iterate over each property found on the Entity class
                foreach (IMutableProperty property in entityType.GetProperties())
                {
                    if (property.PropertyInfo == null)
                    {
                        continue;
                    }

                    if (property.IsPrimaryKey() && IsPrimaryKey(property.PropertyInfo))
                    {
                        // At this point we know that the property is a primary key
                        // let's set it to AutoIncrement on insert.
                        modelBuilder.Entity(entityType.ClrType)
                                    .Property(property.Name)
                                    .ValueGeneratedOnAdd()
                                    .Metadata.BeforeSaveBehavior = PropertySaveBehavior.Ignore;
                    }
                    else if (property.PropertyInfo.PropertyType.IsBoolean())
                    {
                        // Since MySQL stores bool as tinyint, let's add a converter so the tinyint is treated as boolean
                        modelBuilder.Entity(entityType.ClrType)
                                    .Property(property.Name)
                                    .HasConversion(new BoolToZeroOneConverter<short>());
                    }
                }

            }
        }

        private static bool IsPrimaryKey(PropertyInfo property)
        {
            var identityTypes = new List<Type> {
                typeof(short),
                typeof(int),
                typeof(long)
            };

            return property.Name.Equals("Id", StringComparison.CurrentCultureIgnoreCase) && identityTypes.Contains(property.PropertyType);
        }
    }

    public static class TypeExtensions
    {
        public static bool IsBoolean(this Type type)
        {
            Type t = Nullable.GetUnderlyingType(type) ?? type;

            return t == typeof(bool);
        }

        public static bool IsTrueEnum(this Type type)
        {
            Type t = Nullable.GetUnderlyingType(type) ?? type;

            return t.IsEnum;
        }
    }
}
