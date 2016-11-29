using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01Log4Net.Models
{
    public class Log4netContext : DbContext
    {
        public DbSet<Log> Logs { get; set; }
    }
}
