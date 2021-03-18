using EMP.Data.Models.Employees;

namespace EMP.Data.Repos {
    public interface IUnitOfWorkEmployees {
        IRepository<DeptManager> DeptManagerRepository { get; }
        IRepository<VwDeptManagerDetail> DeptManagerDetailRepository { get; }
        IRepository<Departments> DepartmentsRepository { get; }
        IRepository<VwDeptEmpCurrent> DeptEmpRepository { get; }
        IRepository<VwDeptManagerCurrent> DeptManagerCurrentRepository { get; }
        IRepository<VwEmpDetails> EmployeeDetailRepository { get; }
        IRepository<VwEmpDetailsShort> EmployeeDetailShortRepository { get; }
        IRepository<Employees> EmployeeRepository { get; }
        IRepository<VwTitlesCurrent> TitleRepository { get; }
        IRepository<DistinctTitles> DistinctTitleRepository { get; }
        IRepository<DistinctGenders> DistinctGenderRepository { get; }
        IRepository<VwSalariesCurrent> SalaryRepository { get; }

        void Commit();
        void Rollback();
    }
}