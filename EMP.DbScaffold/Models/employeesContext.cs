using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EMP.DbScaffold.Models
{
    public partial class employeesContext : DbContext
    {
        public employeesContext()
        {
        }

        public employeesContext(DbContextOptions<employeesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CurrentDeptEmp> CurrentDeptEmp { get; set; }
        public virtual DbSet<Departments> Departments { get; set; }
        public virtual DbSet<DeptEmp> DeptEmp { get; set; }
        public virtual DbSet<DeptEmpCurrent> DeptEmpCurrent { get; set; }
        public virtual DbSet<DeptEmpLatestDate> DeptEmpLatestDate { get; set; }
        public virtual DbSet<DeptManager> DeptManager { get; set; }
        public virtual DbSet<DeptManagerCurrent> DeptManagerCurrent { get; set; }
        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<Salaries> Salaries { get; set; }
        public virtual DbSet<SalariesCurrent> SalariesCurrent { get; set; }
        public virtual DbSet<Titles> Titles { get; set; }
        public virtual DbSet<TitlesCurrent> TitlesCurrent { get; set; }
        public virtual DbSet<VwDeptEmpCurrent> VwDeptEmpCurrent { get; set; }
        public virtual DbSet<VwDeptManagerCurrent> VwDeptManagerCurrent { get; set; }
        public virtual DbSet<VwEmpDetails> VwEmpDetails { get; set; }
        public virtual DbSet<VwEmpDetailsCurrent> VwEmpDetailsCurrent { get; set; }
        public virtual DbSet<VwSalariesCurrent> VwSalariesCurrent { get; set; }
        public virtual DbSet<VwTitlesCurrent> VwTitlesCurrent { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySQL("server=mycompany.cniwlvrfgzdc.us-east-1.rds.amazonaws.com;uid=appuser;password=Soil9303;port=3306;database=employees;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CurrentDeptEmp>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("current_dept_emp");

                entity.Property(e => e.DeptNo)
                    .IsRequired()
                    .HasColumnName("dept_no")
                    .HasMaxLength(4)
                    .IsFixedLength();

                entity.Property(e => e.EmpNo).HasColumnName("emp_no");

                entity.Property(e => e.FromDate)
                    .HasColumnName("from_date")
                    .HasColumnType("date");

                entity.Property(e => e.ToDate)
                    .HasColumnName("to_date")
                    .HasColumnType("date");
            });

            modelBuilder.Entity<Departments>(entity =>
            {
                entity.HasKey(e => e.DeptNo)
                    .HasName("PRIMARY");

                entity.ToTable("departments");

                entity.HasIndex(e => e.DeptName)
                    .HasName("dept_name")
                    .IsUnique();

                entity.Property(e => e.DeptNo)
                    .HasColumnName("dept_no")
                    .HasMaxLength(4)
                    .IsFixedLength();

                entity.Property(e => e.DeptName)
                    .IsRequired()
                    .HasColumnName("dept_name")
                    .HasMaxLength(40);
            });

            modelBuilder.Entity<DeptEmp>(entity =>
            {
                entity.HasKey(e => new { e.EmpNo, e.DeptNo })
                    .HasName("PRIMARY");

                entity.ToTable("dept_emp");

                entity.HasIndex(e => e.DeptNo)
                    .HasName("dept_no");

                entity.HasIndex(e => new { e.EmpNo, e.ToDate })
                    .HasName("dept_emp_emp_no_IDX");

                entity.Property(e => e.EmpNo).HasColumnName("emp_no");

                entity.Property(e => e.DeptNo)
                    .HasColumnName("dept_no")
                    .HasMaxLength(4)
                    .IsFixedLength();

                entity.Property(e => e.FromDate)
                    .HasColumnName("from_date")
                    .HasColumnType("date");

                entity.Property(e => e.ToDate)
                    .HasColumnName("to_date")
                    .HasColumnType("date");

                entity.HasOne(d => d.DeptNoNavigation)
                    .WithMany(p => p.DeptEmp)
                    .HasForeignKey(d => d.DeptNo)
                    .HasConstraintName("dept_emp_ibfk_2");

                entity.HasOne(d => d.EmpNoNavigation)
                    .WithMany(p => p.DeptEmp)
                    .HasForeignKey(d => d.EmpNo)
                    .HasConstraintName("dept_emp_ibfk_1");
            });

            modelBuilder.Entity<DeptEmpCurrent>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("dept_emp_current");

                entity.HasIndex(e => e.DeptNo)
                    .HasName("dept_emp_current_dept_no_IDX");

                entity.HasIndex(e => e.EmpNo)
                    .HasName("dept_emp_current_emp_no_IDX")
                    .IsUnique();

                entity.Property(e => e.DeptNo)
                    .IsRequired()
                    .HasColumnName("dept_no")
                    .HasMaxLength(4)
                    .IsFixedLength();

                entity.Property(e => e.EmpNo).HasColumnName("emp_no");

                entity.Property(e => e.FromDate)
                    .HasColumnName("from_date")
                    .HasColumnType("date");

                entity.Property(e => e.ToDate)
                    .HasColumnName("to_date")
                    .HasColumnType("date");
            });

            modelBuilder.Entity<DeptEmpLatestDate>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("dept_emp_latest_date");

                entity.Property(e => e.EmpNo).HasColumnName("emp_no");

                entity.Property(e => e.FromDate)
                    .HasColumnName("from_date")
                    .HasColumnType("date");

                entity.Property(e => e.ToDate)
                    .HasColumnName("to_date")
                    .HasColumnType("date");
            });

            modelBuilder.Entity<DeptManager>(entity =>
            {
                entity.HasKey(e => new { e.EmpNo, e.DeptNo })
                    .HasName("PRIMARY");

                entity.ToTable("dept_manager");

                entity.HasIndex(e => new { e.DeptNo, e.ToDate })
                    .HasName("dept_manager_dept_no_IDX");

                entity.Property(e => e.EmpNo).HasColumnName("emp_no");

                entity.Property(e => e.DeptNo)
                    .HasColumnName("dept_no")
                    .HasMaxLength(4)
                    .IsFixedLength();

                entity.Property(e => e.FromDate)
                    .HasColumnName("from_date")
                    .HasColumnType("date");

                entity.Property(e => e.ToDate)
                    .HasColumnName("to_date")
                    .HasColumnType("date");

                entity.HasOne(d => d.DeptNoNavigation)
                    .WithMany(p => p.DeptManager)
                    .HasForeignKey(d => d.DeptNo)
                    .HasConstraintName("dept_manager_ibfk_2");

                entity.HasOne(d => d.EmpNoNavigation)
                    .WithMany(p => p.DeptManager)
                    .HasForeignKey(d => d.EmpNo)
                    .HasConstraintName("dept_manager_ibfk_1");
            });

            modelBuilder.Entity<DeptManagerCurrent>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("dept_manager_current");

                entity.HasIndex(e => e.DeptNo)
                    .HasName("dept_manager_current_dept_no_IDX")
                    .IsUnique();

                entity.HasIndex(e => e.EmpNo)
                    .HasName("dept_manager_current_emp_no_IDX")
                    .IsUnique();

                entity.Property(e => e.DeptNo)
                    .IsRequired()
                    .HasColumnName("dept_no")
                    .HasMaxLength(4)
                    .IsFixedLength();

                entity.Property(e => e.EmpNo).HasColumnName("emp_no");

                entity.Property(e => e.FromDate)
                    .HasColumnName("from_date")
                    .HasColumnType("date");

                entity.Property(e => e.ToDate)
                    .HasColumnName("to_date")
                    .HasColumnType("date");
            });

            modelBuilder.Entity<Employees>(entity =>
            {
                entity.HasKey(e => e.EmpNo)
                    .HasName("PRIMARY");

                entity.ToTable("employees");

                entity.Property(e => e.EmpNo).HasColumnName("emp_no");

                entity.Property(e => e.BirthDate)
                    .HasColumnName("birth_date")
                    .HasColumnType("date");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasMaxLength(14);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasColumnName("gender")
                    .HasColumnType("enum('M','F')");

                entity.Property(e => e.HireDate)
                    .HasColumnName("hire_date")
                    .HasColumnType("date");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasMaxLength(16);
            });

            modelBuilder.Entity<Salaries>(entity =>
            {
                entity.HasKey(e => new { e.EmpNo, e.FromDate })
                    .HasName("PRIMARY");

                entity.ToTable("salaries");

                entity.HasIndex(e => new { e.EmpNo, e.ToDate })
                    .HasName("salaries_emp_no_IDX");

                entity.Property(e => e.EmpNo).HasColumnName("emp_no");

                entity.Property(e => e.FromDate)
                    .HasColumnName("from_date")
                    .HasColumnType("date");

                entity.Property(e => e.Salary).HasColumnName("salary");

                entity.Property(e => e.ToDate)
                    .HasColumnName("to_date")
                    .HasColumnType("date");

                entity.HasOne(d => d.EmpNoNavigation)
                    .WithMany(p => p.Salaries)
                    .HasForeignKey(d => d.EmpNo)
                    .HasConstraintName("salaries_ibfk_1");
            });

            modelBuilder.Entity<SalariesCurrent>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("salaries_current");

                entity.HasIndex(e => e.EmpNo)
                    .HasName("salaries_current_emp_no_IDX")
                    .IsUnique();

                entity.HasIndex(e => e.Salary)
                    .HasName("salaries_current_salary_IDX");

                entity.Property(e => e.EmpNo).HasColumnName("emp_no");

                entity.Property(e => e.FromDate)
                    .HasColumnName("from_date")
                    .HasColumnType("date");

                entity.Property(e => e.Salary).HasColumnName("salary");

                entity.Property(e => e.ToDate)
                    .HasColumnName("to_date")
                    .HasColumnType("date");
            });

            modelBuilder.Entity<Titles>(entity =>
            {
                entity.HasKey(e => new { e.EmpNo, e.Title, e.FromDate })
                    .HasName("PRIMARY");

                entity.ToTable("titles");

                entity.HasIndex(e => new { e.EmpNo, e.ToDate })
                    .HasName("titles_emp_no_IDX");

                entity.Property(e => e.EmpNo).HasColumnName("emp_no");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(50);

                entity.Property(e => e.FromDate)
                    .HasColumnName("from_date")
                    .HasColumnType("date");

                entity.Property(e => e.ToDate)
                    .HasColumnName("to_date")
                    .HasColumnType("date");

                entity.HasOne(d => d.EmpNoNavigation)
                    .WithMany(p => p.Titles)
                    .HasForeignKey(d => d.EmpNo)
                    .HasConstraintName("titles_ibfk_1");
            });

            modelBuilder.Entity<TitlesCurrent>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("titles_current");

                entity.HasIndex(e => e.EmpNo)
                    .HasName("titles_current_emp_no_IDX")
                    .IsUnique();

                entity.HasIndex(e => e.Title)
                    .HasName("titles_current_title_IDX");

                entity.Property(e => e.EmpNo).HasColumnName("emp_no");

                entity.Property(e => e.FromDate)
                    .HasColumnName("from_date")
                    .HasColumnType("date");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(50);

                entity.Property(e => e.ToDate)
                    .HasColumnName("to_date")
                    .HasColumnType("date");
            });

            modelBuilder.Entity<VwDeptEmpCurrent>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_dept_emp_current");

                entity.Property(e => e.DeptNo)
                    .IsRequired()
                    .HasColumnName("dept_no")
                    .HasMaxLength(4)
                    .IsFixedLength();

                entity.Property(e => e.EmpNo).HasColumnName("emp_no");

                entity.Property(e => e.FromDate)
                    .HasColumnName("from_date")
                    .HasColumnType("date");

                entity.Property(e => e.ToDate)
                    .HasColumnName("to_date")
                    .HasColumnType("date");
            });

            modelBuilder.Entity<VwDeptManagerCurrent>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_dept_manager_current");

                entity.Property(e => e.DeptNo)
                    .IsRequired()
                    .HasColumnName("dept_no")
                    .HasMaxLength(4)
                    .IsFixedLength();

                entity.Property(e => e.EmpNo).HasColumnName("emp_no");

                entity.Property(e => e.FromDate)
                    .HasColumnName("from_date")
                    .HasColumnType("date");

                entity.Property(e => e.ToDate)
                    .HasColumnName("to_date")
                    .HasColumnType("date");
            });

            modelBuilder.Entity<VwEmpDetails>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_emp_details");

                entity.Property(e => e.BirthDate)
                    .HasColumnName("birth_date")
                    .HasColumnType("date");

                entity.Property(e => e.DeptName)
                    .IsRequired()
                    .HasColumnName("dept_name")
                    .HasMaxLength(40);

                entity.Property(e => e.DeptNo)
                    .IsRequired()
                    .HasColumnName("dept_no")
                    .HasMaxLength(4)
                    .IsFixedLength();

                entity.Property(e => e.EmpNo).HasColumnName("emp_no");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasMaxLength(14);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasColumnName("gender")
                    .HasColumnType("enum('M','F')");

                entity.Property(e => e.HireDate)
                    .HasColumnName("hire_date")
                    .HasColumnType("date");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasMaxLength(16);

                entity.Property(e => e.ManagerEmpNo).HasColumnName("manager_emp_no");

                entity.Property(e => e.ManagerFirstName)
                    .IsRequired()
                    .HasColumnName("manager_first_name")
                    .HasMaxLength(14);

                entity.Property(e => e.ManagerLastName)
                    .IsRequired()
                    .HasColumnName("manager_last_name")
                    .HasMaxLength(16);

                entity.Property(e => e.Salary).HasColumnName("salary");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<VwEmpDetailsCurrent>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_emp_details_current");

                entity.Property(e => e.BirthDate)
                    .HasColumnName("birth_date")
                    .HasColumnType("date");

                entity.Property(e => e.DeptName)
                    .IsRequired()
                    .HasColumnName("dept_name")
                    .HasMaxLength(40);

                entity.Property(e => e.DeptNo)
                    .IsRequired()
                    .HasColumnName("dept_no")
                    .HasMaxLength(4)
                    .IsFixedLength();

                entity.Property(e => e.EmpNo).HasColumnName("emp_no");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasMaxLength(14);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasColumnName("gender")
                    .HasColumnType("enum('M','F')");

                entity.Property(e => e.HireDate)
                    .HasColumnName("hire_date")
                    .HasColumnType("date");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasMaxLength(16);

                entity.Property(e => e.ManagerEmpNo).HasColumnName("manager_emp_no");

                entity.Property(e => e.ManagerFirstName)
                    .IsRequired()
                    .HasColumnName("manager_first_name")
                    .HasMaxLength(14);

                entity.Property(e => e.ManagerLastName)
                    .IsRequired()
                    .HasColumnName("manager_last_name")
                    .HasMaxLength(16);

                entity.Property(e => e.Salary).HasColumnName("salary");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<VwSalariesCurrent>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_salaries_current");

                entity.Property(e => e.EmpNo).HasColumnName("emp_no");

                entity.Property(e => e.FromDate)
                    .HasColumnName("from_date")
                    .HasColumnType("date");

                entity.Property(e => e.Salary).HasColumnName("salary");

                entity.Property(e => e.ToDate)
                    .HasColumnName("to_date")
                    .HasColumnType("date");
            });

            modelBuilder.Entity<VwTitlesCurrent>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_titles_current");

                entity.Property(e => e.EmpNo).HasColumnName("emp_no");

                entity.Property(e => e.FromDate)
                    .HasColumnName("from_date")
                    .HasColumnType("date");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(50);

                entity.Property(e => e.ToDate)
                    .HasColumnName("to_date")
                    .HasColumnType("date");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
