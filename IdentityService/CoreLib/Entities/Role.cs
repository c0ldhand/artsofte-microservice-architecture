using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities
{
    internal class Role
    {
        
        public Guid Id { get; set; }
       
        public User? UserId { get; set; }

        public string? Name { get; set; }
    }
}
