using CoreLib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Interfaces.Repositories
{
    internal interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByEmailAsync (string email);
    }
}
