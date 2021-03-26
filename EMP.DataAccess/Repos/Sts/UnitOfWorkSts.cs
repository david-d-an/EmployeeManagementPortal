using System;
using EMP.Data.Models.Sts;
using EMP.Data.Repos;
using EMP.DataAccess.Context;

namespace EMP.DataAccess.Repos.Sts
{
    public class UnitOfWorkSts : IUnitOfWorkSts, IDisposable {
        private readonly StsContext _stsContext;
        private IRepository<Aspnetusers> _aspNetUsersRepository;
        private IRepository<AspnetDeptManager> _aspnetDeptManagerRepository;
        // private IRepository<Aspnetroleclaims> _aspnetroleclaimsRepository;
        // private IRepository<Aspnetroles> _aspnetrolesRepository;
        // private IRepository<Aspnetuserclaims> _aspnetuserclaimsRepository;
        // private IRepository<Aspnetuserlogins> _aspnetuserloginsRepository;
        // private IRepository<Aspnetuserroles> _aspnetuserrolesRepository;
        // private IRepository<Aspnetusertokens> _aspnetusertokensRepository;

        public UnitOfWorkSts(StsContext stsContext) { 
            _stsContext = stsContext; 
        }

        public IRepository<Aspnetusers> AspnetUsers {
            get { return _aspNetUsersRepository = 
                _aspNetUsersRepository ?? 
                new AspnetUsersRepository(_stsContext);
            }
        }

        public IRepository<AspnetDeptManager> AspnetDeptManager {
            get { return _aspnetDeptManagerRepository = 
                _aspnetDeptManagerRepository ?? 
                new AspnetDeptManagerRepository(_stsContext);
            }
        }

        public IRepository<Aspnetroleclaims> Aspnetroleclaims {
            get {
                throw new NotImplementedException();
            }
        }

        public IRepository<Aspnetroles> Aspnetroles {
            get {
                throw new NotImplementedException();
            }
        }

        public IRepository<Aspnetuserclaims> Aspnetuserclaims {
            get {
                throw new NotImplementedException();
            }
        }

        public IRepository<Aspnetuserlogins> Aspnetuserlogins {
            get {
                throw new NotImplementedException();
            }
        }

        public IRepository<Aspnetuserroles> Aspnetuserroles {
            get {
                throw new NotImplementedException();
            }
        }

        public IRepository<Aspnetusertokens> Aspnetusertokens { 
            get {
                throw new NotImplementedException();
            }
        }

        public void Commit() { 
            _stsContext.SaveChanges();
        }

        public void Rollback() { 
            _stsContext.Dispose(); 
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing) {
            if (!this.disposed) {
                if (disposing) {
                    _stsContext.Dispose();
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
