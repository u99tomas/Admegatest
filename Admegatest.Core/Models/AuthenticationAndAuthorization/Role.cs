﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admegatest.Core.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleDescription { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
