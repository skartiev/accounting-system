using System.Collections.Generic;
using AccountingSystem.Domain.Concrete;
using AccountingSystem.Domain.Entities;

namespace AccountingSystem.Domain.Abstract
{
    public interface IUserRepository
    {
        IEnumerable<User> Users { get; }
        EfDatabaseContext Context { get; set; }

    }
}
