using System;
using EMP.Data.Models.Sts;
using EMP.Data.Repos;
using EMP.DataAccess.Context;

namespace EMP.DataAccess.Repos
{
    public class UnitOfWorkSts : IUnitOfWorkSts, IDisposable {
        private readonly StsContext _stsContext;

        private IRepository<Aspnetusers> _aspNetUsersRepository;

        public UnitOfWorkSts(StsContext stsContext) { 
            _stsContext = stsContext; 
        }

        public IRepository<Aspnetusers> AspNetUsersRepository {
            get { return _aspNetUsersRepository = 
                _aspNetUsersRepository ?? 
                new AspNetUsersRepository(_stsContext);
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
