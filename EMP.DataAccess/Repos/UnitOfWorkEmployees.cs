using System;
using EMP.Data.Models.Employees;
using EMP.Data.Repos;
using EMP.DataAccess.Context;

namespace EMP.DataAccess.Repos
{
    public class UnitOfWorkEmployees : IUnitOfWorkEmployees, IDisposable {
        private readonly EmployeesContext _employeesContext;

        private IRepository<DeptManager> _deptManagerRepository;
        private IRepository<VwDeptManagerDetail> _deptManagerDetailRepository;
        private IRepository<Departments> _departmentsRepository;
        private IRepository<VwDeptEmpCurrent> _deptEmpRepository;
        private IRepository<VwDeptManagerCurrent> _deptManagerCurrentRepository;
        private IRepository<VwEmpDetails> _employeeDetailRepository;
        private IRepository<VwEmpDetailsShort> _employeeDetailShortRepository;
        private IRepository<Employees> _employeeRepository;
        private IRepository<VwTitlesCurrent> _titleRepository;
        private IRepository<DistinctTitles> _distinctTitleRepository;
        private IRepository<DistinctGenders> _distinctGenderRepository;
        private IRepository<VwSalariesCurrent> _salaryRepository;

        public UnitOfWorkEmployees(EmployeesContext employeesContext) { 
            _employeesContext = employeesContext; 
        }

        public IRepository<DeptManager> DeptManagerRepository {
            get { return _deptManagerRepository = 
                _deptManagerRepository ?? 
                new DeptManagerRepository(_employeesContext);
            }
        }

        public IRepository<VwDeptManagerDetail> DeptManagerDetailRepository {
            get { return _deptManagerDetailRepository = 
                _deptManagerDetailRepository ?? 
                new DeptManagerDetailRepository(_employeesContext);
            }
        }

        public IRepository<Departments> DepartmentsRepository {
            get { return _departmentsRepository = 
                _departmentsRepository ?? 
                new DepartmentsRepository(_employeesContext);
            }
        }

        public IRepository<VwDeptEmpCurrent> DeptEmpRepository { 
            get { return _deptEmpRepository = 
                _deptEmpRepository ?? 
                new DeptEmpRepository(_employeesContext);
            }
        }

        public IRepository<VwDeptManagerCurrent> DeptManagerCurrentRepository {
            get { return _deptManagerCurrentRepository = 
                _deptManagerCurrentRepository ?? 
                new DeptManagerCurrentRepository(_employeesContext);
            }
        }

        public IRepository<VwEmpDetails> EmployeeDetailRepository {
            get { return _employeeDetailRepository = 
                _employeeDetailRepository ?? 
                new EmployeeDetailRepository(_employeesContext);
            }
        }

        public IRepository<VwEmpDetailsShort> EmployeeDetailShortRepository {
            get { return _employeeDetailShortRepository = 
                _employeeDetailShortRepository ?? 
                new EmployeeDetailShortRepository(_employeesContext);
            }
        }

        public IRepository<Employees> EmployeeRepository {
            get { return _employeeRepository = 
                _employeeRepository ?? 
                new EmployeeRepository(_employeesContext);
            }
        }
        
        public IRepository<VwTitlesCurrent> TitleRepository {
            get { return _titleRepository = 
                _titleRepository ?? 
                new TitleRepository(_employeesContext);
            }
        }
        
        public IRepository<DistinctTitles> DistinctTitleRepository {
            get { return _distinctTitleRepository = 
                _distinctTitleRepository ?? 
                new DistinctTitleRepository(_employeesContext);
            }
        }
        
        public IRepository<DistinctGenders> DistinctGenderRepository {
            get { return _distinctGenderRepository = 
                _distinctGenderRepository ?? 
                new DistinctGenderRepository(_employeesContext);
            }
        }
        
        public IRepository<VwSalariesCurrent> SalaryRepository {
            get { return _salaryRepository = 
                _salaryRepository ?? 
                new SalaryRepository(_employeesContext);
            }
        }

        public void Commit() { 
            _employeesContext.SaveChanges();
        }

        public void Rollback() { 
            _employeesContext.Dispose(); 
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing) {
            if (!this.disposed) {
                if (disposing) {
                    _employeesContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
