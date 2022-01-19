using Domain.UnitOfWork;
using System.Data.Common;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbConnection _connection;
        private DbTransaction? _transaction;

        public UnitOfWork(DbConnection connection)
        {
            _connection = connection;
            _connection.Open();
        }

        public DbConnection Connection => _connection;
        public DbTransaction? Transaction => _transaction;

        public void Begin()
        {
            _transaction = _connection.BeginTransaction();
        }

        public void Commit()
        {
            if (_transaction != null)
            {
                _transaction.Commit();
            }
        }

        public void Dispose()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
            }

            _transaction = null;
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }
    }
}
