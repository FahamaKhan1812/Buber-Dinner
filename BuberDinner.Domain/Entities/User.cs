﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid(); 
        public string FirstName { get; set; } = String.Empty;
        public string LastName { get; set; } = String.Empty;   
        public string Email { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
    }
}
