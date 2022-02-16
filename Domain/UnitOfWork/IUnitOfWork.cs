using System.Data.Common;

namespace Domain.UnitOfWork
{
    public interface IUnitOfWork
    {
        DbConnection Connection { get; }
        DbTransaction Transaction { get; }
        public void Begin();
        public void Commit();
        public void Dispose();
        public void Rollback();
    }
}
