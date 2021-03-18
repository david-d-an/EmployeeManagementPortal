using EMP.Data.Models.Sts;

namespace EMP.Data.Repos {
    public interface IUnitOfWorkSts {
        IRepository<Aspnetusers> AspNetUsersRepository { get; }

        void Commit();
        void Rollback();
    }
}