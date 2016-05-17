using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingSystem.Domain.Entities
{
    /// <summary>
    /// Rename to Account when delete older
    /// </summary>
    public class Account
    {
        private string Login { get; set; }
        private string Email { get; set; }
        private string Password { get; set; }
    }
}
