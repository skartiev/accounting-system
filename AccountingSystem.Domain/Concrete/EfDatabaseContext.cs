using System.Data.Entity;
using AccountingSystem.Domain.Entities;

namespace AccountingSystem.Domain.Concrete
{
    public class EfDatabaseContext: DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}
