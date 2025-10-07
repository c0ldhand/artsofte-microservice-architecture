using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public Guid RoleId { get; set; }
        public Role? RoleName { get; set; }
        public List<RefreshToken> RefreshTokens { get; set; } = new();

        public DateTime Updated {  get; set; }
        public DateTime Created {  get; set; }


    }
}
