using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountingSystem.Domain.Entities;

namespace AccountingSystem.Domain.Concrete
{
    public class UnitOfWork : IDisposable
    {
        private EfDatabaseContext _context = new EfDatabaseContext();
        private GenericRepository<User> _usersRepository;

        public GenericRepository<User> UsersRepository
        {
            get { return _usersRepository ?? (_usersRepository = new GenericRepository<User>(_context)); }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
