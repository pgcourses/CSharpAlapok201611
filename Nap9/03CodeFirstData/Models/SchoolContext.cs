﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03CodeFirstData.Models
{
    public class SchoolContext : DbContext
    {
        public DbSet<Teacher> Teachers { get; set; }
    }
}
