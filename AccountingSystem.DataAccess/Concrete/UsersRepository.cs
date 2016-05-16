using System.Collections.Generic;
using System.Data.Entity;
using AccountingSystem.Domain.Abstract;
using AccountingSystem.Domain.Entities;

namespace AccountingSystem.Domain.Concrete
{
    public class UsersRepository : IUserRepository
    {
        /// <summary>
        /// Context of DB
        /// </summary>
        public EfDatabaseContext Context { get; set; }

        public UsersRepository()
        {
            Context = new EfDatabaseContext();
        }

        public UsersRepository(EfDatabaseContext context)
        {
            Context = context;
        }
       
        public IEnumerable<User> GetUsers()
        {
            return Context.Users;
        }

        public User GetUserById(int id)
        {
            return Context.Users.Find(id);
        }

        public void AddUser(User user)
        {
            Context.Users.Add(user);
        }

        public void DeleteUser(User user)
        {
            Context.Users.Remove(user);
        }

        public void UpdateUser(User user)
        {
            Context.Entry(user).State = EntityState.Modified;;
        }

        public void Save()
        {
            Context.SaveChanges();
        }
    }
}
