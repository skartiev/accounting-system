using System;
using AccountingSystem.Domain.Entities;

namespace AccountingSystem.Domain.Concrete
{
    public class UnitOfWork : IDisposable
    {
        private readonly EfDatabaseContext _context = new EfDatabaseContext();
        private GenericRepository<User> _usersRepository;
        private GenericRepository<Account> _accountRepository;

        /// <summary>
        /// Repository for Users
        /// </summary>
        public GenericRepository<User> UsersRepository => _usersRepository ?? (_usersRepository = new GenericRepository<User>(_context));

        /// <summary>
        /// Repository for Accounts
        /// </summary>
        public GenericRepository<Account> AccountRepository
            => _accountRepository ?? (_accountRepository = new GenericRepository<Account>(_context));

        /// <summary>
        /// Saving changes of UnitOfWork
        /// </summary>
        public void Save()
        {
            _context.SaveChanges();
        }

        private bool _disposed;

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
