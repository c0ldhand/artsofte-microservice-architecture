﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Entities
{
    internal class RefreshToken
    {
        public Guid Id { get; set; }
        public User? User {  get; set; }

        public string? UserId { get; set; }
        
        public string? Token {  get; set; }

        public DateTime ExpiresOn { get; set; }
        public bool IsRevoked { get; set; } = false;

    }
}
