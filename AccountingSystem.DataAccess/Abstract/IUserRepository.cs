using System.Collections.Generic;
using AccountingSystem.Domain.Concrete;
using AccountingSystem.Domain.Entities;

namespace AccountingSystem.Domain.Abstract
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();
        User GetUserById(int id);
        void AddUser(User user);
        void DeleteUser(User user);
        void UpdateUser(User user);
        void Save();
        EfDatabaseContext Context { get; set; }

    }
}
