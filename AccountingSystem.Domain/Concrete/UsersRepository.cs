using System.Collections.Generic;
using AccountingSystem.Domain.Abstract;
using AccountingSystem.Domain.Entities;

namespace AccountingSystem.Domain.Concrete
{
    public class UsersRepository : IUserRepository
    {
        public EfDatabaseContext Context { get; set; }
        public IEnumerable<User> Users { get; set; }

        public UsersRepository()
        {
            Context = new EfDatabaseContext();
            Users = Context.Users;
        }
    }
}
