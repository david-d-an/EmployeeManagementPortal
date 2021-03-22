using System;
using EMP.Data.Repos;
using EMP.DataAccess.Context;
using Em = EMP.Data.Models.Employees;

namespace EMP.DataAccess.Repos.Employees
{
    public class UnitOfWorkEmployees : IUnitOfWorkEmployees, IDisposable {
        private readonly EmployeesContext _employeesContext;

        private IRepository<Em.DeptManager> _deptManagerRepository;
        private IRepository<Em.VwDeptManagerDetail> _deptManagerDetailRepository;
        private IRepository<Em.Departments> _departmentsRepository;
        private IRepository<Em.VwDeptEmpCurrent> _deptEmpRepository;
        private IRepository<Em.VwDeptManagerCurrent> _deptManagerCurrentRepository;
        private IRepository<Em.VwEmpDetails> _employeeDetailRepository;
        private IRepository<Em.VwEmpDetailsShort> _employeeDetailShortRepository;
        private IRepository<Em.Employees> _employeeRepository;
        private IRepository<Em.VwTitlesCurrent> _titleRepository;
        private IRepository<Em.DistinctTitles> _distinctTitleRepository;
        private IRepository<Em.DistinctGenders> _distinctGenderRepository;
        private IRepository<Em.VwSalariesCurrent> _salaryRepository;

        public UnitOfWorkEmployees(EmployeesContext employeesContext) { 
            _employeesContext = employeesContext; 
        }

        public IRepository<Em.DeptManager> DeptManagerRepository {
            get { return _deptManagerRepository = 
                _deptManagerRepository ?? 
                new DeptManagerRepository(_employeesContext);
            }
        }

        public IRepository<Em.VwDeptManagerDetail> DeptManagerDetailRepository {
            get { return _deptManagerDetailRepository = 
                _deptManagerDetailRepository ?? 
                new DeptManagerDetailRepository(_employeesContext);
            }
        }

        public IRepository<Em.Departments> DepartmentsRepository {
            get { return _departmentsRepository = 
                _departmentsRepository ?? 
                new DepartmentsRepository(_employeesContext);
            }
        }

        public IRepository<Em.VwDeptEmpCurrent> DeptEmpRepository { 
            get { return _deptEmpRepository = 
                _deptEmpRepository ?? 
                new DeptEmpRepository(_employeesContext);
            }
        }

        public IRepository<Em.VwDeptManagerCurrent> DeptManagerCurrentRepository {
            get { return _deptManagerCurrentRepository = 
                _deptManagerCurrentRepository ?? 
                new DeptManagerCurrentRepository(_employeesContext);
            }
        }

        public IRepository<Em.VwEmpDetails> EmployeeDetailRepository {
            get { return _employeeDetailRepository = 
                _employeeDetailRepository ?? 
                new EmployeeDetailRepository(_employeesContext);
            }
        }

        public IRepository<Em.VwEmpDetailsShort> EmployeeDetailShortRepository {
            get { return _employeeDetailShortRepository = 
                _employeeDetailShortRepository ?? 
                new EmployeeDetailShortRepository(_employeesContext);
            }
        }

        public IRepository<Em.Employees> EmployeeRepository {
            get { return _employeeRepository = 
                _employeeRepository ?? 
                new EmployeeRepository(_employeesContext);
            }
        }
        
        public IRepository<Em.VwTitlesCurrent> TitleRepository {
            get { return _titleRepository = 
                _titleRepository ?? 
                new TitleRepository(_employeesContext);
            }
        }
        
        public IRepository<Em.DistinctTitles> DistinctTitleRepository {
            get { return _distinctTitleRepository = 
                _distinctTitleRepository ?? 
                new DistinctTitleRepository(_employeesContext);
            }
        }
        
        public IRepository<Em.DistinctGenders> DistinctGenderRepository {
            get { return _distinctGenderRepository = 
                _distinctGenderRepository ?? 
                new DistinctGenderRepository(_employeesContext);
            }
        }
        
        public IRepository<Em.VwSalariesCurrent> SalaryRepository {
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
