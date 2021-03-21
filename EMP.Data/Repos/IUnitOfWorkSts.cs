using EMP.Data.Models.Sts;

namespace EMP.Data.Repos {
    public interface IUnitOfWorkSts {
        IRepository<Aspnetusers> AspnetUsers { get; }
        IRepository<AspnetDeptManager> AspnetDeptManager { get; }

        // IRepository<Aspnetroleclaims> Aspnetroleclaims { get; }
        // IRepository<Aspnetroles> Aspnetroles { get; }
        // IRepository<Aspnetuserclaims> Aspnetuserclaims { get; }
        // IRepository<Aspnetuserlogins> Aspnetuserlogins { get; }
        // IRepository<Aspnetuserroles> Aspnetuserroles { get; }
        // IRepository<Aspnetusertokens> Aspnetusertokens { get; }

        void Commit();
        void Rollback();
    }
}